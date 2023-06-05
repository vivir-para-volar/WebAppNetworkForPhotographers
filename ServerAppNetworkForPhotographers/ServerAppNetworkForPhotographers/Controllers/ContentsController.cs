using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Files;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;
using System.Security.Authentication;
using System.Security.Claims;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class ContentsController : ControllerBase
    {
        private readonly ContentsService _contentsService;

        public ContentsController(DataContext dataContext)
        {
            _contentsService = new ContentsService(dataContext);
        }

        [HttpGet("Posts/User/{photographerId}/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetUserPosts(int photographerId, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetUserContents(photographerId, TypeContent.Post, userId, part));
        }

        [HttpGet("Blogs/User/{photographerId}/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetUserBlogs(int photographerId, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetUserContents(photographerId, TypeContent.Blog, userId, part));
        }

        [HttpGet("Posts/Photographer/{photographerId}/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerPosts(int photographerId, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetPhotographerContents(photographerId, TypeContent.Post, userId, part));
        }

        [HttpGet("Blogs/Photographer/{photographerId}/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerBlogs(int photographerId, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetPhotographerContents(photographerId, TypeContent.Blog, userId, part));
        }

        [HttpGet("Posts/Favourites/{photographerId}/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerFavouritesPosts(int photographerId, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetPhotographerFavouritesContents(photographerId, TypeContent.Post, userId, part));
        }

        [HttpGet("Blogs/Favourites/{photographerId}/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetPhotographerFavouritesBlogs(int photographerId, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetPhotographerFavouritesContents(photographerId, TypeContent.Blog, userId, part));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetContentDto?>> GetContentById(int id)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                return Ok(await _contentsService.GetContentById(id, userId));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpGet("Blogs/MainPhoto/{name}")]
        public async Task<ActionResult> GetBlogMainPhotoByName(string name)
        {
            var filePath = FileInteraction.GetBlogMainPhotoPath(name);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "image/jpeg");
        }

        [HttpPost("Posts/Search/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> SearchPosts(SearchDto searchDto, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.SearchContents(searchDto, TypeContent.Post, userId, part));
        }

        [HttpPost("Blogs/Search/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> SearchBlogs(SearchDto searchDto, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.SearchContents(searchDto, TypeContent.Blog, userId, part));
        }

        [HttpPost("News/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetNews(NewsDto newsDto, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetNews(newsDto, userId, part));
        }

        [HttpPost("Others/{part}")]
        public async Task<ActionResult<List<GetContentForListDto>>> GetOthers(OthersDto othersDto, int part)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.GetOthers(othersDto, userId, part));
        }

        [HttpPost("Posts")]
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
        public async Task<ActionResult<Content>> UpdateBlogMainPhoto(int id, IFormFile photo)
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
