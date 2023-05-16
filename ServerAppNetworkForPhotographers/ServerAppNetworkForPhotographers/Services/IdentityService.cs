﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Identity;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace ServerAppNetworkForPhotographers.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly PhotographersService _photographersService;

        public IdentityService(UserManager<AppUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               IConfiguration configuration,
                               DataContext dataContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

            _photographersService = new PhotographersService(dataContext, userManager);
        }

        public async Task<List<GetAppUserDto>> GetAllAdminsAndEmployees()
        {
            var adminRole = await _roleManager.FindByNameAsync(UserRoles.Admin);
            var employeeRole = await _roleManager.FindByNameAsync(UserRoles.Employee);

            var adminUsers = await _userManager.GetUsersInRoleAsync(adminRole.Name);
            var employeeUsers = await _userManager.GetUsersInRoleAsync(employeeRole.Name);

            var getUsers = new List<GetAppUserDto>();
            foreach (var user in adminUsers)
            {
                getUsers.Add(user.ToGetAppUserDto(UserRoles.Admin));
            }
            foreach (var user in employeeUsers)
            {
                getUsers.Add(user.ToGetAppUserDto(UserRoles.Employee));
            }

            return getUsers;
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
            var user = await Register(registerDto, UserRoles.User);

            var photographer = new CreatePhotographerDto(user.Username, user.Email, user.Id);
            await _photographersService.CreatePhotographer(photographer);

            return user;
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
                throw new NotFoundException(nameof(AppUser), null);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                if (userRole == UserRoles.User)
                {
                    await _photographersService.UpdatePhotographerLastLoginDate(user.Id);
                }

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
                throw new NotFoundException(nameof(AppUser), appUserDto.Id);

            var role = (await _userManager.GetRolesAsync(user))[0];
            if (role == UserRoles.User)
            {
                throw new AuthenticationException();
            }

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

            return user.ToGetAppUserDto(role);
        }

        public async Task DeleteAppUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ??
                throw new NotFoundException(nameof(AppUser), id);

            var role = (await _userManager.GetRolesAsync(user))[0];
            if (role == UserRoles.User)
            {
                throw new AuthenticationException();
            }

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

            return user.ToGetAppUserDto(role);
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