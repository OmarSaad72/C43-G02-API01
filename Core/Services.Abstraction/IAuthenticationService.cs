using Microsoft.Win32;
using Shared.DTOs;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);
    }
}
