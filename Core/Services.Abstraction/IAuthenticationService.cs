using Microsoft.Win32;
using Shared.DTOs;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);

        // Some Functions Important for Angular Project:
        //Get current user
        public Task<UserResultDto> GetUserByEmail(string email);
        //Check if email exist
        public Task<bool> CheckIfEmailExist(string email);
        //Update user address
        public Task<ShippingAddressDto> UpdateUserAddress(ShippingAddressDto addressDto, string email);
        //Get user address
        public Task<ShippingAddressDto> GetUserAddress(string email);
    }
}
