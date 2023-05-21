using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IIdentityController
    {
        Task<ActionResult<List<GetAppUserDto>>> GetAllAdminsAndEmployees();
        Task<ActionResult> CreateRoles();
        Task<ActionResult<GetAppUserDto>> RegisterUser(RegisterDto registerDto);
        Task<ActionResult<GetAppUserDto>> RegisterEmployee(RegisterDto registerDto);
        Task<ActionResult<GetAppUserDto>> RegisterAdmin(RegisterDto registerDto);
        Task<ActionResult<TokenDto>> Login(LoginDto loginDto);
        Task<ActionResult<GetAppUserDto>> UpdateAppUser(UpdateAppUserDto appUserDto);
        Task<ActionResult> UpdatePassword(UpdatePasswordDto updatePassword);
        Task<ActionResult> UpdatePasswordForUser(UpdatePasswordForUserDto updatePassword);
        Task<ActionResult> DeleteAppUser(string id);
    }
}
