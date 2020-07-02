using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleCloud_Monolithic.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Exceptions;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.Invoice.Queries.GetInvoice
{
    public class GetInvoiceQuery : IRequest<IEnumerable<GetInvoiceVM>>
    {
        public Guid MLServiceId { get; set; }
    }

    public class GetTodosQueryHandler : IRequestHandler<GetInvoiceQuery, IEnumerable<GetInvoiceVM>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        private readonly double milisecondCost = 0.001; // TODO - get this value from database


        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetInvoiceVM>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var mlService = await _dbContext.MLServices
                .Include(mlService => mlService.ServiceDetails)
                    .ThenInclude(serviceDetails => serviceDetails.ServiceTasks)
                //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(mlService => mlService.Id == request.MLServiceId)
                ?? throw new NotFoundException(nameof(MLService), request.MLServiceId);

            var tasks = mlService.ServiceDetails.ServiceTasks.ToList();

            var invoice = tasks.Select(task => {
                var timeDifference = task.EndTime.Subtract(task.StartTime).TotalMilliseconds;
                var cost = timeDifference * milisecondCost;
                return new GetInvoiceVM(task.Name, timeDifference, milisecondCost, cost);
            });

            return invoice;
        }
    }
}

