using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.BasketDtos
{
    public record BasketDto
    {
        public string Id { get; init; }

        public ICollection<BasketItemDto> BasketItems { get; init; } = [];
    }
}