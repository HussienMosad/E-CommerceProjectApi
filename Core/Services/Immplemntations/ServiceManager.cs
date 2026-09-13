using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Immplemntations
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper , IBasketRepository _basketrepository
       , UserManager<User> _userManager  , IOptions<JwtOptions> _options , IConfiguration _configuration ) //: IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketrepository, _mapper));
        private readonly Lazy<IAuthenticationServices> _AuthService = new Lazy<IAuthenticationServices>(() => new AuthenticationService(_userManager , _options , _mapper));

        private readonly Lazy<IOrderServices> _orderServices = new Lazy<IOrderServices>(() => new OrderServices(_unitOfWork, _mapper , _basketrepository ));

        private readonly Lazy<IPaymentServices> _paymentServices = new Lazy<IPaymentServices>(() => new PaymentServices(_configuration, _basketrepository, _unitOfWork, _mapper));
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationServices AuthenticationService => _AuthService.Value;

        public IOrderServices OrderServices => _orderServices.Value;

        public IPaymentServices PaymentsServices => _paymentServices.Value; 
    }
}