using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Identity;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IIdentityController
    {
        Task<ActionResult> CreateRoles();
        Task<ActionResult<GetAppUserDto>> RegisterUser(RegisterDto registerDto);
        Task<ActionResult<GetAppUserDto>> RegisterEmployee(RegisterDto registerDto);
        Task<ActionResult<GetAppUserDto>> RegisterAdmin(RegisterDto registerDto);
        Task<ActionResult<AppUser>> Login(LoginDto loginDto);
        Task<ActionResult<Photographer>> UpdateAppUser(UpdateAppUserDto appUserDto);
        Task<ActionResult<AppUser>> DeleteAppUser(string id);
    }
}
