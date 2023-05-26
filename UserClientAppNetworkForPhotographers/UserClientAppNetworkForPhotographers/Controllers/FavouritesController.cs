using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.Data;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class FavouritesController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> CreateFavourite(int contentId)
        {
            var favouriteDto = new FavouriteDto(AppUser.GetPhotographerId(HttpContext), contentId);

            try
            {
                await ApiFavourites.Create(favouriteDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteFavourite(int contentId)
        {
            var favouriteDto = new FavouriteDto(AppUser.GetPhotographerId(HttpContext), contentId);

            try
            {
                await ApiFavourites.Delete(favouriteDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
