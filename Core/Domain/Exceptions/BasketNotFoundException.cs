using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string Id) : base($"Can Not Find Basket With Id : {Id}")
        {
            
        }
    }
}
