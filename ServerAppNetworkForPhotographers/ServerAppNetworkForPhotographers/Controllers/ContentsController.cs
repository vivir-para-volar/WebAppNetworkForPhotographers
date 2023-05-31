﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ContentsController : ControllerBase
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
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                return Ok(await _contentsService.GetUserContents(photographerId, TypeContent.Post, userId));
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
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                return Ok(await _contentsService.GetUserContents(photographerId, TypeContent.Blog, userId));
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
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                return Ok(await _contentsService.GetPhotographerContents(photographerId, TypeContent.Post, userId));
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
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                return Ok(await _contentsService.GetPhotographerContents(photographerId, TypeContent.Blog, userId));
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
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                return Ok(await _contentsService.GetPhotographerFavouritesContents(photographerId, TypeContent.Post, userId));
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
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                return Ok(await _contentsService.GetPhotographerFavouritesContents(photographerId, TypeContent.Blog, userId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
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

        [HttpPost("Posts/Search")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> SearchPosts(SearchDto searchDto)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.SearchContents(searchDto, TypeContent.Post, userId));
        }

        [HttpPost("Blogs/Search")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<List<GetContentForListDto>>> SearchBlogs(SearchDto searchDto)
        {
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            if (userId == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(await _contentsService.SearchContents(searchDto, TypeContent.Blog, userId));
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
