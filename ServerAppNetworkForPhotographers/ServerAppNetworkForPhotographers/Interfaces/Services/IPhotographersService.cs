using ServerAppNetworkForPhotographers.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotographersService
    {
        Task<Photographer?> GetPhotographerById(int id);
        Task<Photographer> CreatePhotographer(CreatePhotographerDto photographerDto);
        Task<Photographer> UpdatePhotographer(UpdatePhotographerDto photographerDto);
        Task<Photographer> UpdatePhotographerPhoto(int id);
        Task DeletePhotographer(int id);
    }
}
