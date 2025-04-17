using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UnAuthorizedException();
            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (result == false) throw new UnAuthorizedException();
            return new UserResultDto(user.DisplayName, "Token", user.Email);
        }

        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
            var user = new User()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto(user.DisplayName, "Token", user.Email);
        }
    }
}
