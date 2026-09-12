using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Controllers
{
    public class PaymentController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        => Ok(await _serviceManager.PaymentsServices.CreateOrUpdatePaymentIntentIdAsync(BasketId));
    }
}
