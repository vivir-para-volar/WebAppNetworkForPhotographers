using ServerAppNetworkForPhotographers.Models.Identity.Dtos;
using System.IdentityModel.Tokens.Jwt;

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
        Task DeleteAppUser(string id);
    }
}
