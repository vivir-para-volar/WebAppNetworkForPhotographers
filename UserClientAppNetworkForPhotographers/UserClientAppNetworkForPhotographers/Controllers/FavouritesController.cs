using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using UserClientAppNetworkForPhotographers.Models.Lists;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class FavouritesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var favourites = new GetFavouritesDto();

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);
                var token = AppUser.GetToken(HttpContext);

                favourites.Posts = await ApiFavourites.GetPosts(userId, 1, token);
                favourites.Posts.ForEach(item => item.AppUserId = userId);

                favourites.Blogs = await ApiFavourites.GetBlogs(userId, 1, token);
                favourites.Blogs.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(favourites);
        }

        public async Task<ActionResult> GetPosts(int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiFavourites.GetPosts(userId, part, AppUser.GetToken(HttpContext));
                contents.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }

        public async Task<ActionResult> GetBlogs(int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiFavourites.GetBlogs(userId, part, AppUser.GetToken(HttpContext));
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
