using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.ProductDtos
{
   public record TypeResultDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
