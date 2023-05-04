﻿using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
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

        public async Task<Photographer> CreatePhotographer(CreatePhotographerDto photographerDto)
        {
            if (await CheckExistenceUsername(photographerDto.Username))
            {
                throw new UniqueFieldException("username", photographerDto.Username);
            }

            if (await CheckExistenceEmail(photographerDto.Email))
            {
                throw new UniqueFieldException("email", photographerDto.Email);
            }

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

        public async Task<Photographer> UpdatePhotographer(UpdatePhotographerDto updatedPhotographer)
        {
            var photographer = (await GetPhotographerById(updatedPhotographer.Id)) ??
                throw new PhotographerNotFoundException(updatedPhotographer.Id);

            if (await CheckExistenceUsername(updatedPhotographer.Username, photographer.Id))
            {
                throw new UniqueFieldException("username", updatedPhotographer.Username);
            }

            if (await CheckExistenceEmail(updatedPhotographer.Email, photographer.Id))
            {
                throw new UniqueFieldException("email", updatedPhotographer.Email);
            }

            photographer.Update(updatedPhotographer);

            _context.Entry(photographer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographer;
        }

        public async Task<Photographer> UpdatePhotographerPhoto(int id)
        {
            var photographer = (await GetPhotographerById(id)) ?? 
                throw new PhotographerNotFoundException(id);

            throw new NotImplementedException();
        }

        public async Task DeletePhotographer(int id)
        {
            var photographer = (await GetPhotographerById(id)) ?? 
                throw new PhotographerNotFoundException(id);

            _context.Photographers.Remove(photographer);
            await _context.SaveChangesAsync();
        }


        private async Task<bool> CheckExistenceUsername(string username, int photographerId = -1)
        {
            return await _context.Photographers.AnyAsync(item => item.Id != photographerId && item.Username == username);
        }

        private async Task<bool> CheckExistenceEmail(string email, int photographerId = -1)
        {
            return await _context.Photographers.AnyAsync(item => item.Id != photographerId && item.Email == email);
        }
    }
}
