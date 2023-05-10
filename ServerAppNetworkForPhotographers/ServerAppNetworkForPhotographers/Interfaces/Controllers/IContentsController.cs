using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.Content;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IContentsController
    {
        Task<ActionResult<GetContentDto?>> GetContentById(int id);
        Task<ActionResult<Content>> CreateContentPost(CreateContentPostDto contentPostDto);
        Task<ActionResult<Content>> CreateContentBlog(CreateContentBlogDto contentBlogDto);
        Task<ActionResult<string>> UpdateBlogMainPhoto(int id, IFormFile photo);
        Task<ActionResult> DeleteContent(int id);
    }
}
