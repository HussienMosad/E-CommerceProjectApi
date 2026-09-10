using Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Specifications
{
    internal class OrderWithIncludeSpecifications : BaseSpecifications<Order , Guid>
    {
        public OrderWithIncludeSpecifications(Guid Id) : base(o => o.Id == Id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);

        }
        public OrderWithIncludeSpecifications(string UserEmail) : base(o => o.UserEmail == UserEmail)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
