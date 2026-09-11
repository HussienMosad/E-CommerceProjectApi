using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string UserEmail) : base($"User With Email : {UserEmail} Not Found.")
        {
            
        }
    }
}
