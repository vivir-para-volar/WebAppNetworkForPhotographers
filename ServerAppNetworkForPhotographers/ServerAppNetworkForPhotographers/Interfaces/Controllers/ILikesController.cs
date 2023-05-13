using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ILikesController
    {
        Task<ActionResult<List<GetPhotographerForListDto>>> GetAllContentLikes(int contentId);
        Task<ActionResult<Like>> CreateLike(LikeDto likeDto);
        Task<ActionResult> DeleteLike(LikeDto likeDto);
    }
}
