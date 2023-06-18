using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Files;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Identity;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;
using System.Security.Authentication;
using System.Security.Claims;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotographersController : ControllerBase
    {
        private readonly PhotographersService _photographersService;

        public PhotographersController(DataContext dataContext, UserManager<AppUser> userManager)
        {
            _photographersService = new PhotographersService(dataContext, userManager);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Photographer?>> GetPhotographerById(int id)
        {
            try
            {
                return Ok(await _photographersService.GetPhotographerById(id));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpGet("Photo/{name}")]
        public async Task<ActionResult> GetPhotographerPhotoByName(string name)
        {
            var filePath = FileInteraction.GetProfilePhotoPath(name);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "image/jpeg");
        }

        [HttpPost("Search/{part}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetPhotographerForListDto>>> SearchPhotographers(SearchDto searchDto, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _photographersService.SearchPhotographers(searchDto, userId, part));
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Photographer>> UpdatePhotographer(UpdatePhotographerDto photographerDto)
        {
            try
            {
                return Ok(await _photographersService.UpdatePhotographer(photographerDto));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new FieldResponse(ex.Field, ex.Message));
            }
        }

        [HttpPut("Photo/{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Photographer>> UpdatePhotographerPhoto(int id, IFormFile photo)
        {
            try
            {
                return Ok(await _photographersService.UpdatePhotographerPhoto(id, photo));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> DeletePhotographer(int id)
        {
            try
            {
                await _photographersService.DeletePhotographer(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
