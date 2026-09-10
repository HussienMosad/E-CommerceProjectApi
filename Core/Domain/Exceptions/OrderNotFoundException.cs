using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid OrderId) : base($"Order With Id {OrderId} Not Found")
        {
            
        }
    }
}
