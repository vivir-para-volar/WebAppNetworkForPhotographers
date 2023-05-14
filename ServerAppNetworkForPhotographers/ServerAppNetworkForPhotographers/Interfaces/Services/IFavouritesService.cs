using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IFavouritesService
    {
        Task<Favourite> CreateFavourite(FavouriteDto favouriteDto);
        Task DeleteFavourite(FavouriteDto favouriteDto);
    }
}
