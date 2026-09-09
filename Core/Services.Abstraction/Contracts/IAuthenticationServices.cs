using Shared.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
    public  interface IAuthenticationServices
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);

        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    }
}
