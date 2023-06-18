using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;
using UserClientAppNetworkForPhotographers.Models.Lists;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class ContentsController : Controller
    {
        public async Task<ActionResult> Post(int id)
        {
            GetContentDto post;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                post = await ApiContents.GetById(id, token);
                if (post.Type != TypeContent.Post)
                {
                    return RedirectToAction(nameof(CommonController.ApiError), "General", new { status = StatusCodes.Status400BadRequest });
                }

                post.Comments = await ApiComments.GetAllForContent(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "General", ex.ToObj());
            }

            post.AppUserId = AppUser.GetPhotographerId(HttpContext);

            return View(post);
        }

        public async Task<ActionResult> Blog(int id)
        {
            GetContentDto blog;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                blog = await ApiContents.GetById(id, token);
                if (blog.Type != TypeContent.Blog)
                {
                    return RedirectToAction(nameof(CommonController.ApiError), "General", new { status = StatusCodes.Status400BadRequest });
                }

                blog.Comments = await ApiComments.GetAllForContent(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "General", ex.ToObj());
            }

            blog.AppUserId = AppUser.GetPhotographerId(HttpContext);

            return View(blog);
        }

        public async Task<ActionResult> CreatePost()
        {
            var contentDto = new CreateContentDto();
            contentDto.PhotographerId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contentDto.CategoryDirs = await ApiCategories.GetAllWithDirs(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(contentDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost(CreateContentDto contentDto)
        {
            Content post;
            try
            {
                post = await ApiContents.CreatePost(contentDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, post);
        }

        public async Task<ActionResult> CreateBlog()
        {
            var contentDto = new CreateContentDto();
            contentDto.PhotographerId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contentDto.CategoryDirs = await ApiCategories.GetAllWithDirs(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(contentDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBlog(CreateContentDto contentDto)
        {
            Content post;
            try
            {
                post = await ApiContents.CreateBlog(contentDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, post);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBlog(UpdateContentBlogDto contentBlogDto)
        {
            Content blog;
            try
            {
                blog = await ApiContents.UpdateBlog(contentBlogDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, blog);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBlogMainPhoto(int contentId, IFormFile photo)
        {
            Content blog;
            try
            {
                blog = await ApiContents.UpdateBlogMainPhoto(contentId, photo, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, blog);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePhoto(int contentId, IFormFile photo, CreatePhotoInfoDto photoInfoDto)
        {
            Photo createdPhoto;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                createdPhoto = await ApiPhotos.Create(contentId, photo, token);

                photoInfoDto.PhotoId = createdPhoto.Id;
                await ApiPhotos.CreatePhotoInfo(photoInfoDto, token);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, createdPhoto);
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ApiContents.Delete(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(ProfilesController.Index), "Profiles");
        }
    }
}
