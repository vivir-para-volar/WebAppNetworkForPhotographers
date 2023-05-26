using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.Data;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Likes;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class LikesController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> CreateLike(int contentId)
        {
            var likeDto = new LikeDto(AppUser.GetPhotographerId(HttpContext), contentId);

            try
            {
                await ApiLikes.Create(likeDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLike(int contentId)
        {
            var likeDto = new LikeDto(AppUser.GetPhotographerId(HttpContext), contentId);

            try
            {
                await ApiLikes.Delete(likeDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
