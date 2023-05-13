using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ILikesService
    {
        Task<List<GetPhotographerForListDto>> GetAllContentLikes(int contentId);
        Task<Like> CreateLike(LikeDto likeDto);
        Task DeleteLike(LikeDto likeDto);
    }
}
