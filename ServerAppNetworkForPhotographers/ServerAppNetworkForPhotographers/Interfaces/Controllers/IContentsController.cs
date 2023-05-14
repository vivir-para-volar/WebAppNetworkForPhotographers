using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IContentsController
    {
        Task<ActionResult<List<GetContentForListDto>>> GetPhotographerPosts(int photographerId);
        Task<ActionResult<List<GetContentForListDto>>> GetPhotographerBlogs(int photographerId);
        Task<ActionResult<GetContentDto?>> GetContentById(int id);
        Task<ActionResult<Content>> CreateContentPost(CreateContentPostDto contentPostDto);
        Task<ActionResult<Content>> CreateContentBlog(CreateContentBlogDto contentBlogDto);
        Task<ActionResult<string>> UpdateBlogMainPhoto(int id, IFormFile photo);
        Task<ActionResult<Content>> UpdateContentStatus(int id);
        Task<ActionResult> DeleteContent(int id);
    }
}
