using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
    public interface IOrderServices
    {
        //GetById ==> Take Guid Id ==> Return OrderResult
        Task<OrderResult> GetOrderByIdAsync(Guid OrderId);



        //GetAllByEmail ==> Take string Email ==> Return IEnumerable<OrderResult>
        Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string UserEmail);



        //CreateOrder ==> Take OrderRequest, string Email ==> Return OrderResult
        Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest , string  UserEmail);



        //GetDeliveryMethods ==> Return IEnumerable<DeliveryMethodResult>
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
