using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Lists;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Comments;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Likes;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class ContentActionsController : Controller
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

        [HttpPost]
        public async Task<ActionResult> CreateComment(string text, int contentId)
        {
            var createComment = new CreateCommentDto(text, AppUser.GetPhotographerId(HttpContext), contentId);

            GetCommentDto comment;
            try
            {
                comment = await ApiComments.Create(createComment, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, comment);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteComment(int id)
        {
            try
            {
                await ApiComments.Delete(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
