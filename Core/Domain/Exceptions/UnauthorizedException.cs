using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public sealed class UnauthorizedException : Exception
    {
        public UnauthorizedException(string Message = $"Invalid Email or Password") : base(Message)
        {
            
        }
    }
}
