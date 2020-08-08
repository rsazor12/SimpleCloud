//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.CQRS;
//using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Models.HandlerResponse;
//using Payment_SimpleCloud_MicroservicesMessageBroker.Domain.Entities;
//using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Exceptions;
//using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces;
//using System;
//using System.Diagnostics;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Behaviours
//{
//    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    {
//        private readonly Stopwatch _timer;
//        private readonly ILogger<TRequest> _logger;
//        // private readonly ICurrentUserService _currentUserService;
//        //private readonly IIdentityService _identityService;
//        private readonly IPaymentDbContext _dbContext;

//        public RequestPerformanceBehaviour(
//            ILogger<TRequest> logger, 
//            // ICurrentUserService currentUserService,
//            //IIdentityService identityService,
//            IPaymentDbContext dbContext)
//        {
//            _timer = new Stopwatch();

//            _logger = logger;
//            // _currentUserService = currentUserService;
//            // _identityService = identityService;
//            _dbContext = dbContext;
//        }

//        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
//        {
//            var startTime = DateTime.UtcNow;
//            _timer.Start();

//            var response = await next();

//            var endTime = DateTime.UtcNow;
//            _timer.Stop();


//            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

//            if(typeof(HandlerResponse).IsAssignableFrom(typeof(TResponse)))
//            {
//                (response as HandlerResponse).HandlerExecutionTime = elapsedMilliseconds;


//                // Store info about usage to database
//                if (typeof(IBillableRequest).IsAssignableFrom(typeof(TRequest)))
//                {
//                    // request = (TRequest)(request as IBillableRequestBase);
//                    var billableRequest = request as IBillableRequest;

//                    var mlService = await _dbContext.MLServices
//                       .Include(mlService => mlService.ServiceDetails)
//                       .Include(mlService => mlService.Client)
//                       //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
//                       .FirstOrDefaultAsync(mlService => mlService.Id == billableRequest.MLServiceId)
//                       ?? throw new NotFoundException(nameof(MLService), billableRequest.MLServiceId);

//                    var task = new ClientTask(typeof(TRequest).Name, startTime, endTime);
//                    _dbContext.ServiceTasks.Add(task);
//                    mlService.AddTask(task);

//                    await _dbContext.SaveChangesAsync(cancellationToken);
//                }
//            }    


//            //if (elapsedMilliseconds > 500)
//            //{
//            //    var requestName = typeof(TRequest).Name;
//            //    var userId = _currentUserService.UserId ?? string.Empty;
//            //    var userName = string.Empty;

//            //    if (!string.IsNullOrEmpty(userId))
//            //    {
//            //        userName = await _identityService.GetUserNameAsync(userId);
//            //    }

//            //    _logger.LogWarning("Payment_SimpleCloud_MicroservicesMessageBroker Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
//            //        requestName, elapsedMilliseconds, userId, userName, request);
//            //}

//            return response;
//        }
//    }
//}
