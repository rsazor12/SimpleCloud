using System;
using System.Collections.Generic;
using System.Text;

namespace Identity_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse
{
    public class CommandHandlerResponse : HandlerResponse
    {
        public long DatabaseExecutionTime { get; set; }
    }
    public class CommandHandlerResponse<TResponse> : CommandHandlerResponse
    {
        public TResponse Response { get; set; }
    }
}
