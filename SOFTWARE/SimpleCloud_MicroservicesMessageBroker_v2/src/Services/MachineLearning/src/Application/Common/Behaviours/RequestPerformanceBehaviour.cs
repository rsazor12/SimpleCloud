using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.CQRS;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        // private readonly ICurrentUserService _currentUserService;
        //private readonly IIdentityService _identityService;
        private readonly IMachineLearningDbContext _dbContext;
        private readonly Payment_SimpleCloud_MicroservicesHttp.ClientTasksClient _paymentClientsHttpClient;
        private readonly AppSettings _appSettings;

        public RequestPerformanceBehaviour(
            ILogger<TRequest> logger, 
            // ICurrentUserService currentUserService,
            //IIdentityService identityService,
            IMachineLearningDbContext dbContext,
            IOptions<AppSettings> settings)
        {
            _timer = new Stopwatch();

            _logger = logger;
            // _currentUserService = currentUserService;
            // _identityService = identityService;
            _dbContext = dbContext;
            _appSettings = settings.Value;
            // _machineLearningClientsHttpClient = new ClientsClient(_appSettings.MachineLearningApi, new HttpClient());
            _paymentClientsHttpClient = new Payment_SimpleCloud_MicroservicesHttp.ClientTasksClient(new HttpClient()) { BaseUrl = _appSettings.PaymentApi };

        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var startTime = DateTime.UtcNow;
            _timer.Start();

            var response = await next();

            var endTime = DateTime.UtcNow;
            _timer.Stop();


            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if(typeof(HandlerResponse).IsAssignableFrom(typeof(TResponse)))
            {
                (response as HandlerResponse).HandlerExecutionTime = elapsedMilliseconds;


                // Store info about usage to database
                if (typeof(IBillableRequest).IsAssignableFrom(typeof(TRequest)))
                {
                    // request = (TRequest)(request as IBillableRequestBase);
                    var billableRequest = request as IBillableRequest;

                    var mlService = await _dbContext.MLServices
                       .Include(mlService => mlService.ServiceDetails)
                       .Include(mlService => mlService.Client)
                       //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(mlService => mlService.Id == billableRequest.MLServiceId)
                       ?? throw new NotFoundException(nameof(MLService), billableRequest.MLServiceId);

                    var task = new ServiceTask(typeof(TRequest).Name, startTime, endTime);
                    _dbContext.ServiceTasks.Add(task);
                    mlService.AddTask(task);

                    await AnnounceTaskExecutedAsync(mlService.Client, task.Name, task.StartTime, task.EndTime);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }    


            //if (elapsedMilliseconds > 500)
            //{
            //    var requestName = typeof(TRequest).Name;
            //    var userId = _currentUserService.UserId ?? string.Empty;
            //    var userName = string.Empty;

            //    if (!string.IsNullOrEmpty(userId))
            //    {
            //        userName = await _identityService.GetUserNameAsync(userId);
            //    }

            //    _logger.LogWarning("MachineLearning_SimpleCloud_MicroservicesHttp Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
            //        requestName, elapsedMilliseconds, userId, userName, request);
            //}

            return response;
        }

        public async Task AnnounceTaskExecutedAsync(Client client, string taskName, DateTime startTime, DateTime endTime)
        {
            await _paymentClientsHttpClient.CreateClientAsync(new Payment_SimpleCloud_MicroservicesHttp.AddClientTaskCommand() { ClientEmail = client.Email, TaskName = taskName, StartTIme = startTime, EndTime = endTime});
        }
    }
}
