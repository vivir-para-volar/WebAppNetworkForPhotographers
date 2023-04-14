using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Services
{
    public class PhotographersService : IPhotographersService
    {
        private readonly DataContext _context;

        public PhotographersService(DataContext context)
        {
            _context = context;
        }

        public async Task<Photographer?> GetPhotographerById(int id)
        {
            return await _context.Photographers.FindAsync(id);
        }

        public async Task<Photographer> CreatePhotographer(CreatePhotographerDto newPhotographer)
        {
            if (await CheckExistenceUsername(newPhotographer.Username))
            {
                throw new InvalidOperationException("Photographer with this username already exists");
            }

            if (await CheckExistenceEmail(newPhotographer.Email))
            {
                throw new InvalidOperationException("Photographer with this email already exists");
            }

            var photographer = new Photographer(newPhotographer);

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

        public async Task<Photographer> UpdatePhotographer(UpdatePhotographerDto updatedPhotographer)
        {
            var photographer = (await GetPhotographerById(updatedPhotographer.Id)) ??
                throw new NullReferenceException("Photographer with this id was not found");

            if (await CheckExistenceUsername(updatedPhotographer.Username, photographer.Id))
            {
                throw new InvalidOperationException("Photographer with this username already exists");
            }

            if (await CheckExistenceEmail(updatedPhotographer.Email, photographer.Id))
            {
                throw new InvalidOperationException("Photographer with this email already exists");
            }

            photographer.Update(updatedPhotographer);

            _context.Entry(photographer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographer;
        }

        public async Task<Photographer> UpdatePhotographerLastLoginDate(int id)
        {
            var photographer = (await GetPhotographerById(id)) ??
                throw new NullReferenceException("Photographer with this id was not found");

            photographer.LastLoginDate = DateTime.Now;

            _context.Entry(photographer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographer;

        }

        public async Task<Photographer> UpdatePhotographerProfilePhoto(int id)
        {
            var photographer = (await GetPhotographerById(id)) ??
                throw new NullReferenceException("Photographer with this id was not found");

            throw new NotImplementedException();
        }

        public async Task DeletePhotographer(int id)
        {
            var photographer = (await GetPhotographerById(id)) ??
                throw new NullReferenceException("Photographer with this id was not found");

            _context.Photographers.Remove(photographer);
            await _context.SaveChangesAsync();
        }


        private async Task<bool> CheckExistenceUsername(string username, int photographerID = -1)
        {
            return await _context.Photographers.AnyAsync(item => item.Id != photographerID && item.Username == username);
        }

        private async Task<bool> CheckExistenceEmail(string email, int photographerID = -1)
        {
            return await _context.Photographers.AnyAsync(item => item.Id != photographerID && item.Email == email);
        }
    }
}
