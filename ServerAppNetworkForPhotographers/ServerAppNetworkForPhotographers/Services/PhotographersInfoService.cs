using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.PhotographersInfo;
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

        public async Task<PhotographerInfo> UpdatePhotographerInfo(UpdatePhotographerInfoDto updatedPhotographerInfo)
        {
            var photographerInfo = (await GetPhotographerInfoByPhotographerId(updatedPhotographerInfo.Id)) ??
                throw new KeyNotFoundException("PhotographerInfo with this id was not found");

            photographerInfo.Update(updatedPhotographerInfo);

            _context.Entry(photographerInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographerInfo;
        }
    }
}
