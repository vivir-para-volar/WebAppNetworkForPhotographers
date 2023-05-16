using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Identity;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase, IIdentityController
    {
        private readonly IdentityService _identityService;

        public IdentityController(UserManager<AppUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  IConfiguration configuration,
                                  DataContext dataContext)
        {
            _identityService = new IdentityService(userManager, roleManager, configuration, dataContext);
        }

        [HttpGet("AdminsEmployees")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<List<GetAppUserDto>>> GetAllAdminsAndEmployees()
        {
            return await _identityService.GetAllAdminsAndEmployees();
        }


        [HttpPost("Roles")]
        public async Task<ActionResult> CreateRoles()
        {
            await _identityService.CreateRoles();
            return NoContent();
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<GetAppUserDto>> RegisterUser(RegisterDto registerDto)
        {
            try
            {
                return Ok(await _identityService.RegisterUser(registerDto));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new UniqueFieldResponse(ex.Field, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerResponse(ex.Message));
            }
        }

        [HttpPost("RegisterEmployee")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<GetAppUserDto>> RegisterEmployee(RegisterDto registerDto)
        {
            try
            {
                return Ok(await _identityService.RegisterEmployee(registerDto));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new UniqueFieldResponse(ex.Field, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerResponse(ex.Message));
            }
        }

        [HttpPost("RegisterAdmin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<GetAppUserDto>> RegisterAdmin(RegisterDto registerDto)
        {
            try
            {
                return Ok(await _identityService.RegisterAdmin(registerDto));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new UniqueFieldResponse(ex.Field, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerResponse(ex.Message));
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<JwtSecurityToken>> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _identityService.Login(loginDto);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (NotFoundException)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<GetAppUserDto>> UpdateAppUser(UpdateAppUserDto appUserDto)
        {
            try
            {
                return Ok(await _identityService.UpdateAppUser(appUserDto));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new UniqueFieldResponse(ex.Field, ex.Message));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAppUser(string id)
        {
            try
            {
                await _identityService.DeleteAppUser(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }

            return NoContent();
        }
    }
}
