using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsService _commentsService;

        public CommentsController(DataContext dataContext)
        {
            _commentsService = new CommentsService(dataContext);
        }

        [HttpGet("Content/{contentId}")]
        public async Task<ActionResult<List<GetCommentDto>>> GetAllContentComments(int contentId)
        {
            try
            {
                return Ok(await _commentsService.GetAllContentComments(contentId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetCommentDto>> CreateComment(CreateCommentDto commentDto)
        {
            GetCommentDto comment;

            try
            {
                comment = await _commentsService.CreateComment(commentDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return CreatedAtAction(null, comment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            try
            {
                await _commentsService.DeleteComment(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
