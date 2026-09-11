using Shared.Dtos.AuthenticationDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
    public  interface IAuthenticationServices
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);

        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        Task<UserResultDto> GetCurrentUserAsync(string UserEmail);

        Task<bool> CheckEmailExistAsync(string UserEmail);

        Task<AddressDto> GetAddressAsync(string UserEmail);

        Task<AddressDto> UpdateUserAddressAsync(string UserEmail, AddressDto AddressDto);
    }
}
