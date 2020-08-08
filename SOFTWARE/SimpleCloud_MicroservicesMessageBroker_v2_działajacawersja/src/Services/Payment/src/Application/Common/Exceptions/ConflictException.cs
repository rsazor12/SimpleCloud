using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions
{
    public class ConflictException: Exception
    {
        public ConflictException()
        {

        }

        public ConflictException(string message): base(message)
        {
        }

        public ConflictException(string message, Exception innerException): base(message, innerException)
        {
        }

        public ConflictException(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists.")
        {
        }
    }
}
