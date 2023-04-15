using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Photographers;
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
        public async Task<ActionResult<Photographer>> GetPhotographerById(int id)
        {
            var photographer = await _photographersService.GetPhotographerById(id);

            return photographer == null ? NotFound() : Ok(photographer);
        }

        [HttpPost]
        public async Task<ActionResult<Photographer>> CreatePhotographer(CreatePhotographerDto newPhotographer)
        {
            Photographer photographer;

            try
            {
                photographer = await _photographersService.CreatePhotographer(newPhotographer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetPhotographerById), new { id = photographer.Id }, photographer);
        }

        [HttpPut]
        public async Task<ActionResult<Photographer>> UpdatePhotographer(UpdatePhotographerDto updatedPhotographer)
        {
            try
            {
                return await _photographersService.UpdatePhotographer(updatedPhotographer);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ProfilePhoto/{id}")]
        public async Task<ActionResult<Photographer>> UpdatePhotographerProfilePhoto(int id)
        {
            try
            {
                return await _photographersService.UpdatePhotographerProfilePhoto(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("LastLoginDate/{id}")]
        public async Task<ActionResult<Photographer>> UpdatePhotographerLastLoginDate(int id)
        {
            try
            {
                return await _photographersService.UpdatePhotographerLastLoginDate(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotographer(int id)
        {
            try
            {
                await _photographersService.DeletePhotographer(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
