using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase, IPhotosController
    {
        private readonly PhotosService _photosService;

        public PhotosController(DataContext dataContext)
        {
            _photosService = new PhotosService(dataContext);
        }

        [HttpPost("{contentId}")]
        public async Task<ActionResult<List<Photo>>> CreatePhotos(int contentId, List<IFormFile> photos)
        {
            try
            {
                return Ok(await _photosService.CreatePhotos(contentId, photos));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
