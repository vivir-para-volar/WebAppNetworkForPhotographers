using Microsoft.AspNetCore.Identity;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;

namespace ServerAppNetworkForPhotographers.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser() { }

        public AppUser(RegisterDto registerDto)
        {
            UserName = registerDto.Username;
            Email = registerDto.Email;
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public void Update(UpdateAppUserDto appUserDto)
        {
            UserName = appUserDto.Username;
            Email = appUserDto.Email;
        }

        public GetAppUserDto ToGetAppUserDto(string role)
        {
            return new GetAppUserDto(Id, UserName, Email, role);
        }
    }
}
