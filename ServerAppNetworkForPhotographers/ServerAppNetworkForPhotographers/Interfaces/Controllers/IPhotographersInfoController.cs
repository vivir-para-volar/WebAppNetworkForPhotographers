using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Dtos.PhotographersInfo;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotographersInfoController
    {
        Task<ActionResult<PhotographerInfo>> GetPhotographerInfoByPhotographerId(int photographerId);
        Task<ActionResult<PhotographerInfo>> UpdatePhotographerInfo(UpdatePhotographerInfoDto updatedPhotographerInfo);
    }
}
