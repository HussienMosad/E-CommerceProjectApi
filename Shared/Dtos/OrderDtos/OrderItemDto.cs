using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.OrderDtos
{
    public record OrderItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
