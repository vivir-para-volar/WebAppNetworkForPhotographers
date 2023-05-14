using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IFavouritesService
    {
        Task<List<GetContentForListDto>> GetPhotographerFavouritesPosts(int photographerId);
        Task<List<GetContentForListDto>> GetPhotographerFavouritesBlogs(int photographerId);
        Task<Favourite> CreateFavourite(FavouriteDto favouriteDto);
        Task DeleteFavourite(FavouriteDto favouriteDto);
    }
}
