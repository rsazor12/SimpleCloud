﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Exceptions;
using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Models.HandlerResponse;
using Identity_SimpleCloud_MicroservicesMessageBroker.Domain.Entities;
using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces;
using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Clients.Commands.CreateClient;
using MachineLearning_SimpleCloud_Broker;
using System.Net.Http;
using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Configurations;
using Microsoft.Extensions.Options;

namespace Identity_SimpleCloud_MicroservicesMessageBroker.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiController
    {
        private readonly IIdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;
        private readonly ClientsClient _machineLearningClientsHttpClient;
        private readonly Payment_SimpleCloud_MicroservicesMessageBroker.ClientsClient _paymentClientsHttpClient;
        private readonly AppSettings _appSettings;

        public ClientsController(IIdentityDbContext dbContext, IMapper mapper, IOptions<AppSettings> settings)
        {
            _identityDbContext = dbContext;
            _mapper = mapper;
            _appSettings = settings.Value;
            _machineLearningClientsHttpClient = new ClientsClient(_appSettings.MachineLearningApi, new HttpClient());
            _paymentClientsHttpClient = new Payment_SimpleCloud_MicroservicesMessageBroker.ClientsClient(new HttpClient()) { BaseUrl = _appSettings.PaymentApi};
        }

        [HttpPost]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClient(Application.Clients.Commands.CreateClient.CreateClientCommand command)
        {
            var user = await _identityDbContext.Clients
                .FirstOrDefaultAsync(client => client.Email == command.Email);

            if (user != null)
                throw new ConflictException($"Entity {nameof(user)} with email {command.Email} already exists in database");


            // var newUser = _mapper.Map<Client>(command);
            var newUser = new Client(command.Email, command.Password, command.Name, command.Surname);

            await _identityDbContext.Clients.AddAsync(newUser);

            await AnnounceClientCreatedAsync(newUser);

            await _identityDbContext.SaveChangesAsync(default(CancellationToken));

            return new CommandHandlerResponse<Guid>() { Response = newUser.Id };
        }

        public async Task AnnounceClientCreatedAsync(Client client)
        {
            await _machineLearningClientsHttpClient.CreateClientAsync(new MachineLearning_SimpleCloud_Broker.CreateClientCommand() { Id = client.Id, Email = client.Email });
            await _paymentClientsHttpClient.CreateClientAsync(new Payment_SimpleCloud_MicroservicesMessageBroker.CreateClientCommand() { Email = client.Email });
        }

        //[HttpPost]
        //public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClient(CreateClientCommand command)
        //{
        //    return Ok(await Mediator.Send(command));

        //}

        //[HttpGet]
        //public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClientz(CreateClientCommand command)
        //{
        //    return Ok(await Mediator.Send(command));

        //}
    }
}