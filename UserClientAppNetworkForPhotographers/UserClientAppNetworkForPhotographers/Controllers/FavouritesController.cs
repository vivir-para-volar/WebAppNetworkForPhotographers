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
            var favourites = new ShowFavouritesDto();

            var userId = AppUser.GetPhotographerId(HttpContext);
            var token = AppUser.GetToken(HttpContext);

            try
            {
                favourites.Posts = await ApiFavourites.GetPosts(userId, 1, token);
                favourites.Blogs = await ApiFavourites.GetBlogs(userId, 1, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            favourites.Posts.ForEach(item => item.AppUserId = userId);
            favourites.Blogs.ForEach(item => item.AppUserId = userId);

            return View(favourites);
        }

        public async Task<ActionResult> GetPosts(int part)
        {
            List<GetContentForListDto> contents;

            var userId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contents = await ApiFavourites.GetPosts(userId, part, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            contents.ForEach(item => item.AppUserId = userId);

            return StatusCode(StatusCodes.Status200OK, contents);
        }

        public async Task<ActionResult> GetBlogs(int part)
        {
            List<GetContentForListDto> contents;

            var userId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contents = await ApiFavourites.GetBlogs(userId, part, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            contents.ForEach(item => item.AppUserId = userId);

            return StatusCode(StatusCodes.Status200OK, contents);
        }
    }
}
