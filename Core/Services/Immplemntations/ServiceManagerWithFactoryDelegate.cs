using Microsoft.AspNetCore.Authentication;
using Services.Abstraction.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using IAuthenticationService = Services.Abstraction.Contracts.IAuthenticationServices;

namespace Services.Immplemntations
{
    public class ServiceManagerWithFactoryDelegate(
     Func<IProductService> _productFactory,
     Func<IAuthenticationService> _authFactory,
     Func<IPaymentServices> _paymentFactory,
     Func<IBasketService> _basketFactory,
     Func<IOrderServices> _orderFactory) : IServiceManager
    {
        public IProductService ProductService => _productFactory.Invoke();

        public IBasketService BasketService => _basketFactory.Invoke();

        public IAuthenticationServices AuthenticationService => _authFactory.Invoke();

        public IOrderServices OrderServices => _orderFactory.Invoke();

        public IPaymentServices PaymentsServices => _paymentFactory.Invoke();
    }
}
