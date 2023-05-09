using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotographersService
    {
        Task<Photographer?> GetPhotographerById(int id);
        Task<List<GetPhotographerForList>> SearchPhotographers(SearchPhotographerDto searchPhotographerDto);
        Task<Photographer> CreatePhotographer(CreatePhotographerDto photographerDto);
        Task<Photographer> UpdatePhotographer(UpdatePhotographerDto photographerDto);
        Task<string> UpdatePhotographerPhoto(int id, IFormFile photo);
        Task DeletePhotographer(int id);
    }
}
