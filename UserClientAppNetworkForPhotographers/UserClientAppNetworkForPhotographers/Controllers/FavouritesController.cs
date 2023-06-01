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

                favourites.Posts = await ApiFavourites.GetPosts(AppUser.GetPhotographerId(HttpContext), 1, AppUser.GetToken(HttpContext));
                favourites.Posts.ForEach(item => item.UserId = userId);

                favourites.Blogs = await ApiFavourites.GetBlogs(AppUser.GetPhotographerId(HttpContext), 1, AppUser.GetToken(HttpContext));
                favourites.Blogs.ForEach(item => item.UserId = userId);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(favourites);
        }

        public async Task<ActionResult> GetFavouritesPosts(int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                contents = await ApiFavourites.GetPosts(AppUser.GetPhotographerId(HttpContext), part, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }

        public async Task<ActionResult> GetFavouritesBlogs(int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                contents = await ApiFavourites.GetBlogs(AppUser.GetPhotographerId(HttpContext), part, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }
    }
}
