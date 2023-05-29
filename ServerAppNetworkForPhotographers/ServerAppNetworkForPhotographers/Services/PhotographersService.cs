using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Identity;

namespace ServerAppNetworkForPhotographers.Services
{
    public class PhotographersService
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PhotographersService(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Photographer?> GetPhotographerById(int id)
        {
            return await _context.Photographers.FindAsync(id);
        }

        public async Task<List<GetPhotographerForListDto>> SearchPhotographers(SearchDto searchDto)
        {
            var photographers = await _context.Photographers
                .Where(item => EF.Functions.Like(item.Username, $"%{searchDto.SearchData}%") ||
                               EF.Functions.Like(item.Name, $"%{searchDto.SearchData}%"))
                .ToListAsync();

            return Photographer.ToListGetPhotographerForListDto(photographers);
        }

        public async Task<Photographer> CreatePhotographer(CreatePhotographerDto photographerDto)
        {
            var photographer = new Photographer(photographerDto);

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.Photographers.AddAsync(photographer);
                await _context.SaveChangesAsync();

                var photographerInfo = new PhotographerInfo(photographer.Id);
                await _context.PhotographersInfo.AddAsync(photographerInfo);
                await _context.SaveChangesAsync();

                transaction.Commit();
            }

            return photographer;
        }

        public async Task<Photographer> UpdatePhotographer(UpdatePhotographerDto photographerDto)
        {
            var photographer = (await GetSimplePhotographerById(photographerDto.Id)) ??
                throw new NotFoundException(nameof(Photographer), photographerDto.Id);

            var user = (await GetUserById(photographer.UserId)) ??
                throw new NotFoundException(nameof(AppUser), photographer.UserId);

            if (await CheckExistenceUsername(photographerDto.Username, photographer.UserId))
            {
                throw new UniqueFieldException(nameof(photographerDto.Username), photographerDto.Username);
            }

            if (await CheckExistenceEmail(photographerDto.Email, photographer.UserId))
            {
                throw new UniqueFieldException(nameof(photographerDto.Email), photographerDto.Email);
            }

            // Update user
            user.UserName = photographerDto.Username;
            user.Email = photographerDto.Email;

            await _userManager.UpdateAsync(user);

            // Update photographer
            photographer.Update(photographerDto);

            _context.Entry(photographer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographer;
        }

        public async Task<Photographer> UpdatePhotographerPhoto(int id, IFormFile photo)
        {
            var photographer = (await GetSimplePhotographerById(id)) ??
                throw new NotFoundException(nameof(Photographer), id);

            await photographer.UpdateProfilePhoto(photo);

            _context.Entry(photographer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographer;
        }

        public async Task<Photographer> UpdatePhotographerLastLoginDate(string userId)
        {
            var photographer = (await GetSimplePhotographerByUserId(userId)) ??
                throw new NotFoundException(nameof(Photographer), userId, nameof(Photographer.UserId));

            photographer.LastLoginDate = DateTime.Now;

            _context.Entry(photographer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographer;

        }

        public async Task DeletePhotographer(int id)
        {
            var photographer = (await GetSimplePhotographerById(id)) ??
                throw new NotFoundException(nameof(Photographer), id);

            var user = (await GetUserById(photographer.UserId)) ??
                throw new NotFoundException(nameof(AppUser), photographer.UserId);

            // DeleteUser
            await _userManager.DeleteAsync(user);

            // DeletePhotographer
            photographer.DeleteProfilePhoto();
            await DeletePhotographerActions(id);

            _context.Photographers.Remove(photographer);
            await _context.SaveChangesAsync();
        }

        private async Task<Photographer?> GetSimplePhotographerById(int id)
        {
            return await _context.Photographers.FindAsync(id);
        }

        private async Task<Photographer?> GetSimplePhotographerByUserId(string userId)
        {
            return await _context.Photographers.FirstOrDefaultAsync(item => item.UserId == userId);
        }

        private async Task<AppUser?> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        private async Task<bool> CheckExistenceUsername(string username, string userId)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return false;
            return user.Id != userId;
        }

        private async Task<bool> CheckExistenceEmail(string email, string userId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;
            return user.Id != userId;
        }

        private async Task DeletePhotographerActions(int id)
        {
            // Delete subscriptions
            var subscriptions = await _context.Subscriptions
                .Where(item => item.PhotographerId == id || item.SubscriberId == id)
                .ToListAsync();

            foreach (var subscription in subscriptions)
            {
                _context.Subscriptions.Remove(subscription);
            }

            // Delete comments
            var comments = await _context.Comments.Where(item => item.PhotographerId == id).ToListAsync();
            foreach (var comment in comments)
            {
                _context.Comments.Remove(comment);
            }

            // Delete likes
            var likes = await _context.Likes.Where(item => item.PhotographerId == id).ToListAsync();
            foreach (var like in likes)
            {
                _context.Likes.Remove(like);
            }

            // Delete favourites
            var favourites = await _context.Favourites.Where(item => item.PhotographerId == id).ToListAsync();
            foreach (var favourite in favourites)
            {
                _context.Favourites.Remove(favourite);
            }
        }
    }
}
