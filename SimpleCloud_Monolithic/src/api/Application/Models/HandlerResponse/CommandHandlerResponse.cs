using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Application.Models.HandlerResponse
{
    public class CommandHandlerResponse : HandlerResponse
    {
    }
    public class CommandHandlerResponse<TResponse> : CommandHandlerResponse
    {
        public TResponse Response { get; set; }
    }
}
