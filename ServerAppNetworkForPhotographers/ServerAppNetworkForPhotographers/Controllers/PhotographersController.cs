using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Identity;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class PhotographersController : ControllerBase, IPhotographersController
    {
        private readonly PhotographersService _photographersService;

        public PhotographersController(DataContext dataContext, UserManager<AppUser> userManager)
        {
            _photographersService = new PhotographersService(dataContext, userManager);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Photographer?>> GetPhotographerById(int id)
        {
            return Ok(await _photographersService.GetPhotographerById(id));
        }

        [HttpPost("Search")]
        public async Task<ActionResult<List<GetPhotographerForListDto>>> SearchPhotographers(SearchDto searchDto)
        {
            return Ok(await _photographersService.SearchPhotographers(searchDto));
        }

        [HttpPut]
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
                return Conflict(new UniqueFieldResponse(ex.Field, ex.Message));
            }
        }

        [HttpPut("Photo/{id}")]
        public async Task<ActionResult<string>> UpdatePhotographerPhoto(int id, IFormFile photo)
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
