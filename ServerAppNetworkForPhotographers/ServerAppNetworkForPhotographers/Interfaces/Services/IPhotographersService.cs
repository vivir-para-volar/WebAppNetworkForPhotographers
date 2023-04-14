using ServerAppNetworkForPhotographers.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotographersService
    {
        Task<Photographer?> GetPhotographerById(int id);
        Task<Photographer> CreatePhotographer(CreatePhotographerDto newPhotographer);
        Task<Photographer> UpdatePhotographer(UpdatePhotographerDto updatedPhotographer);
        Task<Photographer> UpdatePhotographerProfilePhoto(int id);
        Task<Photographer> UpdatePhotographerLastLoginDate(int id);
        Task DeletePhotographer(int id);
    }
}
