using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IContentsServices
    {
        Task<List<GetContentForListDto>> GetPhotographerPosts(int photographerId);
        Task<List<GetContentForListDto>> GetPhotographerBlogs(int photographerId);
        Task<GetContentDto?> GetContentById(int id);
        Task<Content> CreateContentPost(CreateContentPostDto contentPostDto);
        Task<Content> CreateContentBlog(CreateContentBlogDto contentBlogDto);
        Task<string> UpdateBlogMainPhoto(int id, IFormFile photo);
        Task<Content> UpdateContentStatus(int id);
        Task DeleteContent(int id);
    }
}
