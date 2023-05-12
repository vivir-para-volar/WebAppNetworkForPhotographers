using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Dtos.PhotographersInfo;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotographersInfoController
    {
        Task<ActionResult<PhotographerInfo?>> GetPhotographerInfoByPhotographerId(int photographerId);
        Task<ActionResult<PhotographerInfo>> UpdatePhotographerInfo(UpdatePhotographerInfoDto photographerInfoDto);
    }
}
