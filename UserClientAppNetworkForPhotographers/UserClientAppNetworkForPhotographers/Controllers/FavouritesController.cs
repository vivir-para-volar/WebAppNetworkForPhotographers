using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
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

                favourites.Posts = await ApiFavourites.GetPosts(AppUser.GetPhotographerId(HttpContext), AppUser.GetToken(HttpContext));
                favourites.Posts.ForEach(item => item.UserId = userId);

                favourites.Blogs = await ApiFavourites.GetBlogs(AppUser.GetPhotographerId(HttpContext), AppUser.GetToken(HttpContext));
                favourites.Blogs.ForEach(item => item.UserId = userId);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "General", ex.ToObj());
            }

            return View(favourites);
        }
    }
}
