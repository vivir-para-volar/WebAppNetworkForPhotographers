using ServerAppNetworkForPhotographers.Models.Data.Dtos.Content;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using ServerAppNetworkForPhotographers.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IFavouritesController
    {
        Task<ActionResult<List<GetContentDto>>> GetPhotographerFavouritesPosts(int photographerId);
        Task<ActionResult<List<GetContentDto>>> GetPhotographerFavouritesBlogs(int photographerId);
        Task<ActionResult<Favourite>> CreateFavourite(FavouriteDto favouriteDto);
        Task<ActionResult> DeleteFavourite(FavouriteDto favouriteDto);
    }
}
