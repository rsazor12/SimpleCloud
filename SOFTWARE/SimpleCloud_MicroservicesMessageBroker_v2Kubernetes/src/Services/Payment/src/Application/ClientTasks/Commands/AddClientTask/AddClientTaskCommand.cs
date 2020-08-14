using MediatR;
using Microsoft.EntityFrameworkCore;
using Payment_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Mappings;
using Payment_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Payment_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesHttp.Application.ClientTasks.Commands
{
    public class AddClientTaskCommand : IRequest<CommandHandlerResponse<Guid>>
    {
        public string ClientEmail { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTIme { get; set; }
        public DateTime EndTime { get; set; }

    }

    public class AddClientTaskCommandHandler : IRequestHandler<AddClientTaskCommand, CommandHandlerResponse<Guid>>
    {
        private readonly IPaymentDbContext _dbContext;

        public AddClientTaskCommandHandler(IPaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandHandlerResponse<Guid>> Handle(AddClientTaskCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Clients
                .FirstOrDefaultAsync(client => client.Email == request.ClientEmail);

            if (user == null)
                throw new ConflictException($"Entity {nameof(user)} with email {request.ClientEmail} don't exists in database");


            // var newUser = _mapper.Map<Client>(request);
            var newTask = new ClientTask(request.TaskName, request.StartTIme, request.EndTime);

            await _dbContext.ClientTasks.AddAsync(newTask);

            user.AddTask(newTask);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandHandlerResponse<Guid>() { Response = newTask.Id };

        }

    }
}
