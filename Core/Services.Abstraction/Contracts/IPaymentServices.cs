using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
    public interface IPaymentServices
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentIdAsync(string BasketId);
        Task UpdatePaymentStatusAsync(string json, string signatureHeader);
    }
}
