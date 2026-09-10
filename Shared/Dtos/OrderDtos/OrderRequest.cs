using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.OrderDtos
{
    public record OrderRequest
    {
        public string BasketId { get; init; } = string.Empty;

        public AddressDto ShippingAddress { get; init; }

        public int DeliveryMethodId { get; init; }
    }
}
