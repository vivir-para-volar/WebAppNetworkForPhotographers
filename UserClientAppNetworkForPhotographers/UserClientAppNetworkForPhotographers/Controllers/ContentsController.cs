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
            var contentPostDto = new CreateContentPostDto();
            contentPostDto.PhotographerId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contentPostDto.CategoryDirs = await ApiCategories.GetAllWithDirs(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(contentPostDto);
        }


        [HttpPost]
        public async Task<ActionResult> CreatePost(CreateContentPostDto contentPostDto)
        {
            Content post;
            try
            {
                post = await ApiContents.CreatePost(contentPostDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, post);
        }

        public async Task<ActionResult> CreateBlog()
        {
            var contentBlogDto = new CreateContentBlogDto();
            contentBlogDto.PhotographerId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contentBlogDto.CategoryDirs = await ApiCategories.GetAllWithDirs(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(contentBlogDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBlog(CreateContentBlogDto contentBlogDto)
        {
            Content blog;
            try
            {
                blog = await ApiContents.CreateBlog(contentBlogDto, AppUser.GetToken(HttpContext));
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
            var token = AppUser.GetToken(HttpContext);

            try
            {
                var createdPhoto = await ApiPhotos.Create(contentId, photo, token);

                photoInfoDto.PhotoId = createdPhoto.Id;
                await ApiPhotos.CreatePhotoInfo(photoInfoDto, token);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created);
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
