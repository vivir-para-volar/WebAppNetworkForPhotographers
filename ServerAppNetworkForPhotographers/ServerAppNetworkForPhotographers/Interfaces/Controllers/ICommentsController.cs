using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ICommentsController
    {
        Task<ActionResult<List<GetCommentDto>>> GetAllContentComments(int contentId);
        Task<ActionResult<Comment>> CreateComment(CreateCommentDto commentDto);
        Task<ActionResult> DeleteComment(int id);
    }
}
