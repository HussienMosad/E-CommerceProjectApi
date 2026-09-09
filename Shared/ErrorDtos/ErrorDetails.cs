using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Shared.ErrorDtos
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; }

        public override string ToString()
        => JsonSerializer.Serialize(this);

    }
}
