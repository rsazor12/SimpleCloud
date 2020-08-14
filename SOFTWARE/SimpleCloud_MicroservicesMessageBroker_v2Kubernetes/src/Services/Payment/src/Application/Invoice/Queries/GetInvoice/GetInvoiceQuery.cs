using AutoMapper;
using MediatR;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesHttp.Application.Invoice.Queries.GetInvoice
{
    public class GetInvoiceQuery : IRequest<IEnumerable<GetInvoiceVM>>
    {
        public Guid ClientId { get; set; }
    }

    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, IEnumerable<GetInvoiceVM>>
    {
        private readonly IPaymentDbContext _dbContext;
        private readonly IMapper _mapper;

        private readonly double milisecondCost = 0.001; // TODO - get this value from database


        public GetInvoiceQueryHandler(IPaymentDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetInvoiceVM>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            //var mlService = await _dbContext.MLServices
            //    .Include(mlService => mlService.ServiceDetails)
            //        .ThenInclude(serviceDetails => serviceDetails.ServiceTasks)
            //    //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(mlService => mlService.Id == request.MLServiceId)
            //    ?? throw new NotFoundException(nameof(MLService), request.MLServiceId);

            var clientTasks = _dbContext.ClientTasks.Where(clientTask => clientTask.Client.Id == request.ClientId).ToList();

            // var tasks = mlService.ServiceDetails.ServiceTasks.ToList();

            var invoice = clientTasks.Select(task =>
            {
                var timeDifference = task.EndTime.Subtract(task.StartTime).TotalMilliseconds;
                var cost = timeDifference * milisecondCost;
                return new GetInvoiceVM(task.Name, timeDifference, milisecondCost, cost);
            });

            return invoice;
        }
    }
}

