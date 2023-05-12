using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Identity;
using ServerAppNetworkForPhotographers.Models.Identity.Dtos;
using ServerAppNetworkForPhotographers.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase, IIdentityController
    {
        private readonly IdentityService _identityService;

        public IdentityController(UserManager<AppUser> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  IConfiguration configuration)
        {
            _identityService = new IdentityService(userManager, roleManager, configuration);
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
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _identityService.Login(loginDto);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (AppUserNotFoundException)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        public async Task<ActionResult<Photographer>> UpdateAppUser(UpdateAppUserDto appUserDto)
        {
            try
            {
                return Ok(await _identityService.UpdateAppUser(appUserDto));
            }
            catch (AppUserNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new UniqueFieldResponse(ex.Field, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteAppUser(string id)
        {
            try
            {
                await _identityService.DeleteAppUser(id);
            }
            catch (AppUserNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
