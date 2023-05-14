using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ICommentsService
    {
        Task<List<GetCommentDto>> GetAllContentComments(int contentId);
        Task<Comment> CreateComment(CreateCommentDto commentDto);
        Task DeleteComment(int id);
    }
}
