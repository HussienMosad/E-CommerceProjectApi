using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
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
    }
}
