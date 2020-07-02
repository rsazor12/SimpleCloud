using System;

namespace Identity_SimpleCloud_MicroservicesHttp.Application.Common.CQRS
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
