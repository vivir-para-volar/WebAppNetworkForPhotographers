using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContentsController : ControllerBase, IContentsController
    {
        private readonly ContentsService _contentsService;

        public ContentsController(DataContext dataContext)
        {
            _contentsService = new ContentsService(dataContext);
        }

        [HttpGet("Posts/User/{photographerId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> GetUserPosts(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetUserContents(photographerId, TypeContent.Post));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("Blogs/User/{photographerId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> GetUserBlogs(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetUserContents(photographerId, TypeContent.Blog));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("Posts/Photographer/{photographerId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerPosts(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetPhotographerContents(photographerId, TypeContent.Post));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("Blogs/Photographer/{photographerId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerBlogs(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetPhotographerContents(photographerId, TypeContent.Blog));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("Posts/Favourites/{photographerId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerFavouritesPosts(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetPhotographerFavouritesContents(photographerId, TypeContent.Post));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("Blogs/Favourites/{photographerId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerFavouritesBlogs(int photographerId)
        {
            try
            {
                return Ok(await _contentsService.GetPhotographerFavouritesContents(photographerId, TypeContent.Blog));
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

        [HttpPost("Posts/Search")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> SearchPosts(SearchDto searchDto)
        {
            return Ok(await _contentsService.SearchContents(searchDto, TypeContent.Post));
        }

        [HttpPost("Blogs/Search")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> SearchBlogs(SearchDto searchDto)
        {
            return Ok(await _contentsService.SearchContents(searchDto, TypeContent.Blog));
        }

        [HttpPost("Posts")]
        [Authorize(Roles = UserRoles.User)]
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

        [HttpPost("Blogs")]
        [Authorize(Roles = UserRoles.User)]
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

        [HttpPut("Blogs/MainPhoto/{id}")]
        [Authorize(Roles = UserRoles.User)]
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
        [Authorize(Roles = UserRoles.AdminEmployee)]
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
        [Authorize(Roles = UserRoles.User)]
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
