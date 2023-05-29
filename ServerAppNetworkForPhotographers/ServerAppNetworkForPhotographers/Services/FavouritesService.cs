using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;

namespace ServerAppNetworkForPhotographers.Services
{
    public class FavouritesService
    {
        private readonly DataContext _context;

        public FavouritesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Favourite> CreateFavourite(FavouriteDto favouriteDto)
        {
            if (!await CheckExistencePhotographer(favouriteDto.PhotographerId))
            {
                throw new NotFoundException(nameof(Photographer), favouriteDto.PhotographerId);
            }
            if (!await CheckExistenceContent(favouriteDto.ContentId))
            {
                throw new NotFoundException(nameof(Content), favouriteDto.ContentId);
            }

            if ((await GetFavourite(favouriteDto)) != null)
            {
                throw new UniqueModelException(nameof(Favourite));
            }

            var favourite = new Favourite(favouriteDto);

            await _context.Favourites.AddAsync(favourite);
            await _context.SaveChangesAsync();

            return favourite;
        }

        public async Task DeleteFavourite(FavouriteDto favouriteDto)
        {
            var favourite = (await GetFavourite(favouriteDto)) ??
                throw new NotFoundException(favouriteDto);

            _context.Favourites.Remove(favourite);
            await _context.SaveChangesAsync();
        }

        private async Task<Favourite?> GetFavourite(FavouriteDto favouriteDto)
        {
            return await _context.Favourites
                .FirstOrDefaultAsync(item => item.PhotographerId == favouriteDto.PhotographerId && item.ContentId == favouriteDto.ContentId);
        }

        private async Task<bool> CheckExistencePhotographer(int photographerId)
        {
            return await _context.Photographers.AnyAsync(item => item.Id == photographerId);
        }

        private async Task<bool> CheckExistenceContent(int contentId)
        {
            return await _context.Contents.AnyAsync(item => item.Id == contentId);
        }
    }
}
