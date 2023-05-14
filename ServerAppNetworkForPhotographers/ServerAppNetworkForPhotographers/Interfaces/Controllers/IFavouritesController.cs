﻿using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IFavouritesController
    {
        Task<ActionResult<Favourite>> CreateFavourite(FavouriteDto favouriteDto);
        Task<ActionResult> DeleteFavourite(FavouriteDto favouriteDto);
    }
}
