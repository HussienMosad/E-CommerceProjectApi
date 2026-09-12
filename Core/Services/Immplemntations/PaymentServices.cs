using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketDtos;
using Stripe;
using Product = Domain.Entities.ProductModule.Product;

namespace Services.Immplemntations
{
    public class PaymentServices(IConfiguration _configuration , IBasketRepository _basketRepository
         ,IUnitOfWork _unitOfWork , IMapper _mapper) : IPaymentServices
    {
        #region With Out Helper Methods
        //public async Task<BasketDto> CreateOrUpdatePaymentIntentIdAsync(string BasketId)
        //{
        //    // [1] Set up key [secret key] ==> Stripe key
        //    StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

        //    // [2] Get basket [by basketId]
        //    var Basket = await _basketRepository.GetBasketByIdAsync(BasketId)
        //        ?? throw new BasketNotFoundException(BasketId);

        //    // [3] Validate items price [basket.item.price != product.price] ==> product from db
        //    foreach (var item in Basket.BasketItems)
        //    {
        //        var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(item.Id)
        //            ?? throw new ProductNotFoundException(item.Id);

        //        item.Price = Product.Price;    
        //    }

        //    // [4] Validate shipping price ==> get deliveryMethod [deliveryMethodId]
        //    //     shippingPrice = DeliveryMethod.Price
        //    if (!Basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");
        //    var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
        //        ?? throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);
        //    Basket.ShippingPrice = DeliveryMethod.Price;
        //    // [5] Total ==> [SubTotal + ShippingPrice] ==> cents ==> +100
        //    //     [long] ([basket.items.price] + shippingPrice) * 100
        //    var amount = (long)(Basket.BasketItems.Sum(i => i.Quantity * i.Price) + Basket.ShippingPrice) * 100;

        //    // [6] Create or update paymentIntent
        //    //     Save changes [Update PaymentIntentId]
        //    var stripeService = new PaymentIntentService();

        //    if (string.IsNullOrEmpty(Basket.PaymentIntentId))
        //    {
        //        // Create
        //        var options = new PaymentIntentCreateOptions
        //        {
        //            Amount = amount, // total = subtotal + shippingPrice
        //            Currency = "USD", // dollar
        //            PaymentMethodTypes = new List<string> { "card" }
        //        };

        //        var paymentIntent = await stripeService.CreateAsync(options);

        //        Basket.PaymentIntentId = paymentIntent.Id;
        //        Basket.ClientSecret = paymentIntent.ClientSecret;
        //    }
        //    else
        //    {
        //        // Update
        //        var options = new PaymentIntentUpdateOptions
        //        {
        //            Amount = amount
        //        };

        //        await stripeService.UpdateAsync(
        //            Basket.PaymentIntentId,
        //            options);
        //    }

        //    // [7] Save changes [Update] Basket
        //    await _basketRepository.CreateOrUpdateBasket(Basket);

        //    // [8] Map to BasketDto ==> return
        //    return _mapper.Map<BasketDto>(Basket);
        //}
        #endregion


        public async Task<BasketDto> CreateOrUpdatePaymentIntentIdAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration
    .GetSection("StripeSettings")["SecretKey"];

            var Basket = await GetBasketAsync(BasketId);

            await ValidateBasketAsync(Basket);

            var amount = CalculateTotalAsync(Basket);

            await CreationOrUpdatePaymentIntentAsync(Basket, amount);

            await _basketRepository.CreateOrUpdateBasket(Basket);

            return _mapper.Map<BasketDto>(Basket);
        }

        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket Basket, long amount)
        {
            var stripeService = new PaymentIntentService();

            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                // Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount, // total = subtotal + shippingPrice
                    Currency = "USD", // dollar
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await stripeService.CreateAsync(options);

                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };

                await stripeService.UpdateAsync(
                    Basket.PaymentIntentId,
                    options);
            }
        }

        private long CalculateTotalAsync(CustomerBasket Basket)
        {
            var amount = (long)(Basket.Items.Sum(i => i.Quantity * i.Price) + Basket.ShippingPrice) * 100;
            return amount;
        }

        private async Task ValidateBasketAsync(CustomerBasket Basket)
        {
            foreach (var item in Basket.Items)
            {
                var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                item.Price = Product.Price;
            }
            if (!Basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);
            Basket.ShippingPrice = DeliveryMethod.Price;

        }

        private async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            return  await _basketRepository.GetBasketByIdAsync(BasketId)
              ?? throw new BasketNotFoundException(BasketId);
        }

    }
}