using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Lists;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Comments;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Likes;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class ContentActionsController : Controller
    {
        public async Task<ActionResult<List<GetPhotographerForListDto>>> GetAllContentLikes(int contentId)
        {
            List<GetPhotographerForListDto> photographersForList;

            try
            {
                photographersForList = await ApiLikes.GetAllForContent(contentId, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, photographersForList);
        }

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




        public async Task<ActionResult> GetNewContentComments(int contentId, string startTime)
        {
            List<GetCommentDto> comments;

            try
            {
                comments = await ApiComments.GetNewForContent(contentId, startTime, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, comments);
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
