using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.AuthenticationDtos
{
    public record UserResultDto(string DisplayName, string Token, string Email);
    
}
