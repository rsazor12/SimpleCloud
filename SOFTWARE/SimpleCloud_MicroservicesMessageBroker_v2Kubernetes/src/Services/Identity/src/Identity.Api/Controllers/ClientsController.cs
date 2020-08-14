using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions;
using Identity_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Identity_SimpleCloud_MicroservicesHttp.Domain.Entities;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Identity_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;
using MachineLearning_SimpleCloud_MicroservicesHttp;
using System.Net.Http;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Identity_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
// using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
//using Identity_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
//using EventBus.Base.Standard;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiController
    {
        private readonly IIdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;
        private readonly ClientsClient _machineLearningClientsHttpClient;
        private readonly Payment_SimpleCloud_MicroservicesHttp.ClientsClient _paymentClientsHttpClient;
        private readonly AppSettings _appSettings;
        private readonly IEventBus _eventBus;

        public ClientsController(
            IIdentityDbContext dbContext,
            IMapper mapper,
            IOptions<AppSettings> settings,
            IEventBus eventBus)
        {
            _identityDbContext = dbContext;
            _mapper = mapper;
            _appSettings = settings.Value;
            _machineLearningClientsHttpClient = new ClientsClient(_appSettings.MachineLearningApi, new HttpClient());
            _paymentClientsHttpClient = new Payment_SimpleCloud_MicroservicesHttp.ClientsClient(new HttpClient()) { BaseUrl = _appSettings.PaymentApi};
            _eventBus = eventBus;
        }

        [HttpPost]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClient(Application.Clients.Commands.CreateClient.CreateClientCommand command)
        {
            var user = await _identityDbContext.Clients
                .FirstOrDefaultAsync(client => client.Email == command.Email);

            if (user != null)
                throw new ConflictException($"Entity {nameof(user)} with email {command.Email} already exists in database");

            var newUser = new Client(command.Email, command.Password, command.Name, command.Surname);

            await _identityDbContext.Clients.AddAsync(newUser);

            await _identityDbContext.SaveChangesAsync(default(CancellationToken));

            await AnnounceClientCreatedAsync(newUser);

            return new CommandHandlerResponse<Guid>() { Response = newUser.Id };
        }

        public async Task AnnounceClientCreatedAsync(Client client)
        {
            var @event = new ClientCreatedIntegrationEvent() {ClientId = client.Id, Email = client.Email };
            _eventBus.Publish(@event);
          }
    }
}