using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotographersInfo;

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
                throw new NotFoundException(nameof(PhotographerInfo), photographerInfoDto.PhotographerId);

            photographerInfo.Update(photographerInfoDto);

            _context.Entry(photographerInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return photographerInfo;
        }
    }
}
