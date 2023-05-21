using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<List<GetAppUserDto>> GetAllAdminsAndEmployees();
        Task CreateRoles();
        Task<GetAppUserDto> RegisterUser(RegisterDto registerDto);
        Task<GetAppUserDto> RegisterEmployee(RegisterDto registerDto);
        Task<GetAppUserDto> RegisterAdmin(RegisterDto registerDto);
        Task<TokenDto> Login(LoginDto loginDto);
        Task<GetAppUserDto> UpdateAppUser(UpdateAppUserDto appUserDto);
        Task UpdatePassword(UpdatePasswordDto updatePassword);
        Task UpdatePasswordForUser(UpdatePasswordForUserDto updatePassword);
        Task DeleteAppUser(string id);
    }
}
