using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotographersController : ControllerBase, IPhotographersController
    {
        private readonly PhotographersService _photographersService;

        public PhotographersController(DataContext dataContext)
        {
            _photographersService = new PhotographersService(dataContext);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Photographer?>> GetPhotographerById(int id)
        {
            return Ok(await _photographersService.GetPhotographerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Photographer>> CreatePhotographer(CreatePhotographerDto photographerDto)
        {
            Photographer photographer;

            try
            {
                photographer = await _photographersService.CreatePhotographer(photographerDto);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(ex.Message);
            }

            return CreatedAtAction(nameof(GetPhotographerById), new { id = photographer.Id }, photographer);
        }

        [HttpPut]
        public async Task<ActionResult<Photographer>> UpdatePhotographer(UpdatePhotographerDto photographerDto)
        {
            try
            {
                return Ok(await _photographersService.UpdatePhotographer(photographerDto));
            }
            catch (PhotographerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("Photo/{id}")]
        public async Task<ActionResult<Photographer>> UpdatePhotographerPhoto(int id)
        {
            try
            {
                return Ok(await _photographersService.UpdatePhotographerPhoto(id));
            }
            catch (PhotographerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotographer(int id)
        {
            try
            {
                await _photographersService.DeletePhotographer(id);
            }
            catch (PhotographerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
