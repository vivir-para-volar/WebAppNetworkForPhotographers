using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Content;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase, IFavouritesController
    {
        private readonly FavouritesService _favouritesService;

        public FavouritesController(DataContext dataContext)
        {
            _favouritesService = new FavouritesService(dataContext);
        }

        [HttpGet("Posts/{photographerId}")]
        public async Task<ActionResult<List<GetContentDto>>> GetPhotographerFavouritesPosts(int photographerId)
        {
            try
            {
                return Ok(await _favouritesService.GetPhotographerFavouritesPosts(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("Blogs/{photographerId}")]
        public async Task<ActionResult<List<GetContentDto>>> GetPhotographerFavouritesBlogs(int photographerId)
        {
            try
            {
                return Ok(await _favouritesService.GetPhotographerFavouritesBlogs(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
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
