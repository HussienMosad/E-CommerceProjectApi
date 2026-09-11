using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.AuthenticationDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        // Post ===> Register 
        [HttpPost("Register")]
       public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
            => Ok( await _serviceManager.AuthenticationService.RegisterAsync(registerDto));

        // post ===> Login

        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => Ok( await _serviceManager.AuthenticationService.LoginAsync(loginDto));

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
    => Ok(await _serviceManager.AuthenticationService.CheckEmailExistAsync(email));


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _serviceManager.AuthenticationService
                .GetCurrentUserAsync(email);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationService.GetAddressAsync(email);
            return Ok(Address);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationService.UpdateUserAddressAsync(email, addressDto);
            return Ok(Address);
        }
    }
}
