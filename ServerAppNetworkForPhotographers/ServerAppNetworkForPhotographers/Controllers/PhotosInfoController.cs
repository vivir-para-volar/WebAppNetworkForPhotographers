using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosInfoController : ControllerBase
    {
        private readonly PhotosInfoService _photosInfoService;

        public PhotosInfoController(DataContext dataContext)
        {
            _photosInfoService = new PhotosInfoService(dataContext);
        }

        [HttpGet("{photoId}")]
        public async Task<ActionResult<PhotoInfo?>> GetPhotoInfo(int photoId)
        {
            return Ok(await _photosInfoService.GetPhotoInfo(photoId));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<PhotoInfo>>> CreatePhotosInfo(List<CreatePhotoInfoDto> photoInfoDtos)
        {
            try
            {
                return Ok(await _photosInfoService.CreatePhotosInfo(photoInfoDtos));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueModelException ex)
            {
                return Conflict(new ConflictResponse(ex.Message));
            }
        }
    }
}
