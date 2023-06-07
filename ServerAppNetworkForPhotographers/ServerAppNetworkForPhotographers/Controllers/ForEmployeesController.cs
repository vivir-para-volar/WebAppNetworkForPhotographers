using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.AdminEmployee)]
    public class ForEmployeesController : ControllerBase
    {
        private readonly ForEmployeesService _employeesService;

        public ForEmployeesController(DataContext dataContext)
        {
            _employeesService = new ForEmployeesService(dataContext);
        }

        [HttpGet("Photographer/{id}")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult<GetPhotographerForListDto?>> GetPhotographerById(int id)
        {
            return Ok(await _employeesService.GetPhotographerById(id));
        }

        [HttpGet("Content/{id}")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult<GetContentForEmployeeDto?>> GetContentById(int id)
        {
            return Ok(await _employeesService.GetContentById(id));
        }

        [HttpPut("Content/Status/{contentId}")]
        public async Task<ActionResult> UpdateContentStatus(int contentId)
        {
            try
            {
                await _employeesService.UpdateContentStatus(contentId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }

        [HttpPut("Photographer/Status/{photographerId}")]
        public async Task<ActionResult> UpdatePhotographerStatus(int photographerId)
        {
            try
            {
                await _employeesService.UpdatePhotographerStatus(photographerId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
