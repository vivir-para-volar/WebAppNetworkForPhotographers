using ServerAppNetworkForPhotographers.Models.Data;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotosService
    {
        Task<List<Photo>> CreatePhotos(int contentId, List<IFormFile> photos);
    }
}
