using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.Content;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IContentServices
    {
        Task<GetContentDto?> GetContentById(int id);
        Task<Content> CreateContentPost(CreateContentPostDto contentPostDto);
        Task<Content> CreateContentBlog(CreateContentBlogDto contentBlogDto);
        Task<string> UpdateBlogMainPhoto(int id, IFormFile photo);
        Task DeleteContent(int id);
    }
}
