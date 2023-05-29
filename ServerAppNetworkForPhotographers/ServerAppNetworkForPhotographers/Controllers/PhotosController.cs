using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Files;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class PhotosController : ControllerBase
    {
        private readonly PhotosService _photosService;

        public PhotosController(DataContext dataContext)
        {
            _photosService = new PhotosService(dataContext);
        }

        [HttpGet("{contentId}/{name}")]
        public async Task<ActionResult> GetPhotoByContentIdAndName(int contentId, string name)
        {
            var filePath = FileInteraction.GetContentPhotoPath(contentId, name);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "image/jpeg");
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
