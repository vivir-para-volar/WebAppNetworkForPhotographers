using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Content;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IFavouritesService
    {
        Task<List<GetContentDto>> GetPhotographerFavouritesPosts(int photographerId);
        Task<List<GetContentDto>> GetPhotographerFavouritesBlogs(int photographerId);
        Task<Favourite> CreateFavourite(FavouriteDto favouriteDto);
        Task DeleteFavourite(FavouriteDto favouriteDto);
    }
}
