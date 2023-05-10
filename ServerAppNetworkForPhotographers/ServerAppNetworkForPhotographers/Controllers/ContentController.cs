using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Dtos.Content;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : Controller, IContentController
    {
        private readonly ContentService _contentService;

        public ContentController(DataContext dataContext)
        {
            _contentService = new ContentService(dataContext);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetContentDto?>> GetContentById(int id)
        {
            return Ok(await _contentService.GetContentById(id));
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Content>> CreateContentPost(CreateContentPostDto contentPostDto)
        {
            Content content;

            try
            {
                content = await _contentService.CreateContentPost(contentPostDto);
            }
            catch (PhotographerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return CreatedAtAction(nameof(GetContentById), new { id = content.Id }, content);
        }

        [HttpPost("Blog")]
        public async Task<ActionResult<Content>> CreateContentBlog(CreateContentBlogDto contentBlogDto)
        {
            Content content;

            try
            {
                content = await _contentService.CreateContentBlog(contentBlogDto);
            }
            catch (PhotographerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return CreatedAtAction(nameof(GetContentById), new { id = content.Id }, content);
        }

        [HttpPut("Blog/MainPhoto/{id}")]
        public async Task<ActionResult<string>> UpdateBlogMainPhoto(int id, IFormFile photo)
        {
            try
            {
                return Ok(await _contentService.UpdateBlogMainPhoto(id, photo));
            }
            catch (ContentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContent(int id)
        {
            try
            {
                await _contentService.DeleteContent(id);
            }
            catch (ContentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
