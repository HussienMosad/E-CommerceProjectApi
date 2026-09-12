using Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentIdSpecfications : BaseSpecifications<Order , Guid>
    {
        public OrderWithPaymentIntentIdSpecfications(string PaymentIntentId) :base(o => o.PaymentIntentId == PaymentIntentId)
        {
            
        }
    }
}
