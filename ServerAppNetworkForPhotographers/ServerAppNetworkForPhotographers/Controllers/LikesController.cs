using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class LikesController : ControllerBase
    {
        private readonly LikesService _likesService;

        public LikesController(DataContext dataContext)
        {
            _likesService = new LikesService(dataContext);
        }

        [HttpGet("Content/{contentId}")]
        public async Task<ActionResult<List<GetPhotographerForListDto>>> GetAllContentLikes(int contentId)
        {
            try
            {
                return Ok(await _likesService.GetAllContentLikes(contentId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<Like>> CreateLike(LikeDto likeDto)
        {
            Like like;

            try
            {
                like = await _likesService.CreateLike(likeDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueModelException ex)
            {
                return Conflict(new ConflictResponse(ex.Message));
            }

            return CreatedAtAction(null, like);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLike(LikeDto likeDto)
        {
            try
            {
                await _likesService.DeleteLike(likeDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
