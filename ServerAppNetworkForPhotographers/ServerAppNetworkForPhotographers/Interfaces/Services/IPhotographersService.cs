using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotographersService
    {
        Task<Photographer?> GetPhotographerById(int id);
        Task<List<GetPhotographerForListDto>> SearchPhotographers(SearchPhotographerDto searchPhotographerDto);
        Task<Photographer> CreatePhotographer(CreatePhotographerDto photographerDto);
        Task<Photographer> UpdatePhotographer(UpdatePhotographerDto photographerDto);
        Task<string> UpdatePhotographerPhoto(int id, IFormFile photo);
        Task DeletePhotographer(int id);
    }
}
