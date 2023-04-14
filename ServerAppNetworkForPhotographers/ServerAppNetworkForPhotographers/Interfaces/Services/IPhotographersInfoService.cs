using ServerAppNetworkForPhotographers.Dtos.PhotographersInfo;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotographersInfoService
    {
        Task<PhotographerInfo?> GetPhotographerInfoByPhotographerId(int photographerId);
        Task<PhotographerInfo> UpdatePhotographerInfo(UpdatePhotographerInfoDto updatedPhotographerInfo);
    }
}
