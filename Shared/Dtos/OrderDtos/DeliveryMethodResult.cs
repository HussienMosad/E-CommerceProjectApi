using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.OrderDtos
{
    public record DeliveryMethodResult
    {
        public int Id { get; init; }

        public string ShortName { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public decimal Cost { get; init; }

        public string DeliveryTime { get; init; } = string.Empty;
    }
}
