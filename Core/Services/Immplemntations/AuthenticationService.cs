using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Common;
using Shared.Dtos.AuthenticationDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Immplemntations
{
    public class AuthenticationService(UserManager<User> _userManager ,
        IOptions<JwtOptions> _options
      ,  IMapper _mapper)      : IAuthenticationServices
    {
       

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Find By Email
            var User = await _userManager.FindByEmailAsync(loginDto.Email);

            if (User is null) throw new UnauthorizedException();

            // Check PassWord 
            var Result = await _userManager.CheckPasswordAsync(User , loginDto.Password);
            if(!Result) throw new UnauthorizedException();

            return new UserResultDto(User.DisplayName, await CreateTokenAsync(User), User.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };
            var result = await _userManager.CreateAsync( user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }

            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            // Cliams   [Name _ Email  _  Roles ]
            // Key ==> SininCreds (Key + Algorithm)
            // Token

            var JWTOptions = _options.Value;
           
            var Claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name , user.DisplayName),

                new Claim(ClaimTypes.Email , user.Email)
            };

            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var Role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, Role));

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTOptions.SecretKey));

            var SignInCreds = new SigningCredentials(Key , SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(issuer: JWTOptions.Issuer, audience: JWTOptions.Audience , claims: Claims, expires: DateTime.UtcNow.AddDays(JWTOptions.ExpirationInDays), signingCredentials: SignInCreds);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }


        public async Task<bool> CheckEmailExistAsync(string UserEmail)
        {
            var User = await _userManager.FindByEmailAsync(UserEmail);
            return User != null;
        }

        public async Task<AddressDto> GetAddressAsync(string UserEmail)
        {
            var User = await _userManager.Users
                .Where(u => u.Email == UserEmail)           // Filter by email
                .Include(u => u.Address)                    // Load the Address navigation property
                .FirstOrDefaultAsync()
                ?? throw new UserNotFoundException(UserEmail);

            return _mapper.Map<AddressDto>(User.Address);
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string UserEmail)
        {
         var User = await _userManager.FindByEmailAsync(UserEmail)
                ?? throw new UserNotFoundException(UserEmail);
            return new UserResultDto(User.DisplayName , await CreateTokenAsync(User) , User.Email);
        }

        public async Task<AddressDto> UpdateUserAddressAsync(string UserEmail, AddressDto AddressDto)
        {
            var User = await _userManager.Users.Include(u => u.Email == UserEmail).FirstOrDefaultAsync()
                ?? throw new UserNotFoundException(UserEmail);
            if (User.Address != null) // He Had Address So Will Update It
            {
                User.Address.FirstName = AddressDto.FirstName;
                     User.Address.LastName = AddressDto.LastName;
                     User.Address.City = AddressDto.City;
                     User.Address.Country = AddressDto.Country;
                     User.Address.Street = AddressDto.Street;
            }
            else // He Dont Have Address So Create It
            {
                var address = _mapper.Map<Address>(AddressDto);
                User.Address = address;
            }
            await _userManager.UpdateAsync(User);

            return _mapper.Map<AddressDto>(User.Address);
        }

    }
}
