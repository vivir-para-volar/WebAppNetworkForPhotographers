using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Identity;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerAppNetworkForPhotographers.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<AppUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }

        public async Task<GetAppUserDto> RegisterUser(RegisterDto registerDto)
        {
            return await Register(registerDto, UserRoles.User);
        }

        public async Task<GetAppUserDto> RegisterEmployee(RegisterDto registerDto)
        {
            return await Register(registerDto, UserRoles.Employee);
        }

        public async Task<GetAppUserDto> RegisterAdmin(RegisterDto registerDto)
        {
            return await Register(registerDto, UserRoles.Admin);
        }

        public async Task<JwtSecurityToken> Login(LoginDto loginDto)
        {
            var user = await FindAppUserByUsernameOrEmail(loginDto.Login);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
            {
                throw new AppUserNotFoundException(null);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public async Task<GetAppUserDto> UpdateAppUser(UpdateAppUserDto appUserDto)
        {
            var user = await _userManager.FindByIdAsync(appUserDto.Id) ??
                throw new AppUserNotFoundException(appUserDto.Id);

            if (await CheckExistenceUsername(appUserDto.Username, user.Id))
            {
                throw new UniqueFieldException(nameof(appUserDto.Username), appUserDto.Username);
            }

            if (await CheckExistenceEmail(appUserDto.Email, user.Id))
            {
                throw new UniqueFieldException(nameof(appUserDto.Email), appUserDto.Email);
            }

            user.Update(appUserDto);
            await _userManager.UpdateAsync(user);

            return user.ToGetAppUserDto();
        }

        public async Task DeleteAppUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ??
                throw new AppUserNotFoundException(id);

            await _userManager.DeleteAsync(user);
        }

        private async Task<GetAppUserDto> Register(RegisterDto registerDto, string role)
        {
            if (await CheckExistenceUsername(registerDto.Username))
            {
                throw new UniqueFieldException(nameof(registerDto.Username), registerDto.Username);
            }

            if (await CheckExistenceEmail(registerDto.Email))
            {
                throw new UniqueFieldException(nameof(registerDto.Email), registerDto.Email);
            }

            var user = new AppUser(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) throw new Exception(result.ToString());

            await _userManager.AddToRoleAsync(user, role);

            return user.ToGetAppUserDto();
        }

        private async Task<AppUser> FindAppUserByUsernameOrEmail(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login);
            }

            return user;
        }

        private async Task<bool> CheckExistenceUsername(string username, string? userId = null)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return false;
            if (userId == null) return true;
            return user.Id != userId;
        }

        private async Task<bool> CheckExistenceEmail(string email, string? userId = null)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;
            if (userId == null) return true;
            return user.Id != userId;
        }
    }
}