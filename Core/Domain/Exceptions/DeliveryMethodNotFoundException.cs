using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException : NotFoundException
    {
        public DeliveryMethodNotFoundException(int Id) : base($"Delivery Method With Id {Id} Is Not Found")
        {
            
        }
    }
}
