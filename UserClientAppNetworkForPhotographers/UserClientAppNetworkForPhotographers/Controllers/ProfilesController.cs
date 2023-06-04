using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using UserClientAppNetworkForPhotographers.Models.Lists;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class ProfilesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ShowPhotographerForProfileDto getPhotographer;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);
                var token = AppUser.GetToken(HttpContext);

                var photographer = await ApiPhotographers.GetById(userId, token);
                photographer.PhotographerInfo = await ApiPhotographers.GetInfoByPhotographerId(photographer.Id, token);


                getPhotographer = new ShowPhotographerForProfileDto(photographer);
                getPhotographer.AppUserId = userId;

                getPhotographer.Posts = await ApiContentsProfile.GetUserPosts(photographer.Id, 1, token);
                getPhotographer.Posts.ForEach(item => item.AppUserId = userId);

                getPhotographer.Blogs = await ApiContentsProfile.GetUserBlogs(photographer.Id, 1, token);
                getPhotographer.Blogs.ForEach(item => item.AppUserId = userId);

            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(getPhotographer);
        }

        public async Task<ActionResult> Photographer(int id)
        {
            if (id == AppUser.GetPhotographerId(HttpContext))
            {
                return RedirectToAction(nameof(Index));
            }

            ShowPhotographerForProfileDto getPhotographer;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);
                var token = AppUser.GetToken(HttpContext);

                var photographer = await ApiPhotographers.GetById(id, token);
                photographer.PhotographerInfo = await ApiPhotographers.GetInfoByPhotographerId(photographer.Id, token);


                getPhotographer = new ShowPhotographerForProfileDto(photographer);
                getPhotographer.AppUserId = userId;

                getPhotographer.Posts = await ApiContentsProfile.GetPhotographerPosts(photographer.Id, 1, token);
                getPhotographer.Posts.ForEach(item => item.AppUserId = userId);

                getPhotographer.Blogs = await ApiContentsProfile.GetPhotographerBlogs(photographer.Id, 1, token);
                getPhotographer.Blogs.ForEach(item => item.AppUserId = userId);

            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(getPhotographer);
        }


        public async Task<ActionResult> GetUserPosts(int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiContentsProfile.GetUserPosts(userId, part, AppUser.GetToken(HttpContext));
                contents.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }

        public async Task<ActionResult> GetUserBlogs(int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiContentsProfile.GetUserBlogs(userId, part, AppUser.GetToken(HttpContext));
                contents.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }


        public async Task<ActionResult> GetPhotographerPosts(int id, int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiContentsProfile.GetPhotographerPosts(id, part, AppUser.GetToken(HttpContext));
                contents.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }

        public async Task<ActionResult> GetPhotographerBlogs(int id, int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiContentsProfile.GetPhotographerBlogs(id, part, AppUser.GetToken(HttpContext));
                contents.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }
    }
}
