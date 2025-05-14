using System;

namespace ApiClientes.Service.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { 
        }
        
    }
}
