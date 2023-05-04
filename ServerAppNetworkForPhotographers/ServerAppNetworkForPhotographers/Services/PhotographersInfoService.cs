using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.PhotographersInfo;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Services
{
    public class PhotographersInfoService : IPhotographersInfoService
    {
        private readonly DataContext _context;

        public PhotographersInfoService(DataContext context)
        {
            _context = context;
        }

        public async Task<PhotographerInfo?> GetPhotographerInfoByPhotographerId(int photographerId)
        {
            return await _context.PhotographersInfo.FirstOrDefaultAsync(item => item.PhotographerId == photographerId);
        }

        public async Task<PhotographerInfo> UpdatePhotographerInfo(UpdatePhotographerInfoDto photographerInfoDto)
        {
            var photographerInfo = (await GetPhotographerInfoByPhotographerId(photographerInfoDto.PhotographerId)) ??
                throw new PhotographerInfoNotFoundException(photographerInfoDto.PhotographerId);

            photographerInfo.Update(photographerInfoDto);

            _context.Entry(photographerInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographerInfo;
        }
    }
}
