using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ErrorDtos
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public IEnumerable<ValidationErrors> Errors { get; set; } = [];
    }
}
