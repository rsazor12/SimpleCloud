using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Application.Common.CQRS
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
