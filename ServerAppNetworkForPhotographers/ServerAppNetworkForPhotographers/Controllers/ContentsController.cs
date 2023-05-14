using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase, IContentsController
    {
        private readonly ContentsService _contentsService;

        public ContentsController(DataContext dataContext)
        {
            _contentsService = new ContentsService(dataContext);
        }

        [HttpGet("Posts/Photographer/{photographerId}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerPosts(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetPhotographerPosts(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("Blogs/Photographer/{photographerId}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerBlogs(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetPhotographerBlogs(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetContentDto?>> GetContentById(int id)
        {
            return Ok(await _contentsService.GetContentById(id));
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Content>> CreateContentPost(CreateContentPostDto contentPostDto)
        {
            Content content;

            try
            {
                content = await _contentsService.CreateContentPost(contentPostDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return CreatedAtAction(nameof(GetContentById), new { id = content.Id }, content);
        }

        [HttpPost("Blog")]
        public async Task<ActionResult<Content>> CreateContentBlog(CreateContentBlogDto contentBlogDto)
        {
            Content content;

            try
            {
                content = await _contentsService.CreateContentBlog(contentBlogDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return CreatedAtAction(nameof(GetContentById), new { id = content.Id }, content);
        }

        [HttpPut("Blog/MainPhoto/{id}")]
        public async Task<ActionResult<string>> UpdateBlogMainPhoto(int id, IFormFile photo)
        {
            try
            {
                return Ok(await _contentsService.UpdateBlogMainPhoto(id, photo));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ConflictResponse(ex.Message));
            }
        }

        [HttpPut("Status/{id}")]
        public async Task<ActionResult<Content>> UpdateContentStatus(int id)
        {
            try
            {
                return Ok(await _contentsService.UpdateContentStatus(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContent(int id)
        {
            try
            {
                await _contentsService.DeleteContent(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
