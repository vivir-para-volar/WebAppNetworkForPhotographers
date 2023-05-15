using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotographersService
    {
        Task<Photographer?> GetPhotographerById(int id);
        Task<List<GetPhotographerForListDto>> SearchPhotographers(SearchDto searchDto);
        Task<Photographer> CreatePhotographer(CreatePhotographerDto photographerDto);
        Task<Photographer> UpdatePhotographer(UpdatePhotographerDto photographerDto);
        Task<string> UpdatePhotographerPhoto(int id, IFormFile photo);
        Task<Photographer> UpdatePhotographerLastLoginDate(string userId);
        Task DeletePhotographer(int id);
    }
}
