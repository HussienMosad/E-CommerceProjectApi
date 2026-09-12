using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationServices AuthenticationService { get;  }
        public IOrderServices OrderServices { get; }
        public IPaymentServices PaymentsServices { get; }
    }
}
