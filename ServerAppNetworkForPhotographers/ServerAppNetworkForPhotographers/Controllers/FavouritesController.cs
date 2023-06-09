﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class FavouritesController : ControllerBase
    {
        private readonly FavouritesService _favouritesService;

        public FavouritesController(DataContext dataContext)
        {
            _favouritesService = new FavouritesService(dataContext);
        }

        [HttpPost]
        public async Task<ActionResult<Favourite>> CreateFavourite(FavouriteDto favouriteDto)
        {
            Favourite favourite;

            try
            {
                favourite = await _favouritesService.CreateFavourite(favouriteDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueModelException ex)
            {
                return Conflict(new ConflictResponse(ex.Message));
            }

            return CreatedAtAction(null, favourite);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteFavourite(FavouriteDto favouriteDto)
        {
            try
            {
                await _favouritesService.DeleteFavourite(favouriteDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
