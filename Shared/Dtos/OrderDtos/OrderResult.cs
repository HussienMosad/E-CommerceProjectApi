using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.OrderDtos
{
    public record OrderResult
    {
        public Guid  Id { get; set; }
        public string UserEmail { get; init; } = string.Empty;

        public AddressDto ShippingAddress { get; init; }

        public ICollection<OrderItemDto> OrderItems { get; init; }
            = new List<OrderItemDto>();

        public String PaymentStatus { get; init; } = String.Empty;

        public String DeliveryMethod { get; init; } = string.Empty;

        public int? DeliveryMethodId { get; init; }

        public decimal SubTotal { get; init; }

        public decimal Total { get; init; }

        // SubTotal = OrderItem quantity * price
        // Total = SubTotal + delivery price

        public DateTimeOffset OrderDate { get; set; }
            = DateTimeOffset.UtcNow;

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
