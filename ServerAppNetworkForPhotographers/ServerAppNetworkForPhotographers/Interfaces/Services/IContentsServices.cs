using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IContentsServices
    {
        Task<List<GetContentForListDto>> GetPhotographerContents(int photographerId, string typeContent);
        Task<List<GetContentForListDto>> GetPhotographerFavouritesContents(int photographerId, string typeContent);
        Task<GetContentDto?> GetContentById(int id);
        Task<List<GetContentForListDto>> SearchContents(SearchDto searchDto, string typeContent);
        Task<Content> CreateContentPost(CreateContentPostDto contentPostDto);
        Task<Content> CreateContentBlog(CreateContentBlogDto contentBlogDto);
        Task<string> UpdateBlogMainPhoto(int id, IFormFile photo);
        Task<Content> UpdateContentStatus(int id);
        Task DeleteContent(int id);
    }
}
