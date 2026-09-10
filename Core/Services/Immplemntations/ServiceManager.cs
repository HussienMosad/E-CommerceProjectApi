using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Immplemntations
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper , IBasketRepository _repository
       , UserManager<User> _userManager  , IOptions<JwtOptions> _options ) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_repository, _mapper));
        private readonly Lazy<IAuthenticationServices> _AuthService = new Lazy<IAuthenticationServices>(() => new AuthenticationService(_userManager , _options));

        private readonly Lazy<IOrderServices> _orderServices = new Lazy<IOrderServices>(() => new OrderServices(_unitOfWork, _mapper , _repository ));
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationServices AuthenticationService => _AuthService.Value;

        public IOrderServices OrderServices => _orderServices.Value;
    }
}