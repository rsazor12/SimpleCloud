using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_Broker.Application.Common.CQRS
{
    public interface IBillableRequest
    {
        public Guid MLServiceId { get; set; }
    }

    //public interface IBillableRequest : IBillableRequestBase, IRequest
    //{

    //}

    //public interface IBillableRequest<TResponse> : IBillableRequestBase, IRequest<TResponse>
    //{

    //}
}
