using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.OrderDtos;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrderController(IServiceManager _manager) : ApiController
    {

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid Id)
        {
            var Order = await _manager.OrderServices.GetOrderByIdAsync(Id);
            return Ok(Order);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrdersByEmailAsync()
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _manager.OrderServices.GetAllOrdersByEmailAsync(UserEmail);
            return Ok(Orders);
        }



       [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest orderRequest)
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _manager.OrderServices.CreateOrderAsync(orderRequest, UserEmail);
            return Ok(Order);
        }



       
        [HttpGet("DeliveryMethods")]
       public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _manager.OrderServices.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }


    }
}
