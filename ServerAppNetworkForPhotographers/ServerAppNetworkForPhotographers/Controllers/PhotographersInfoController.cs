using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.PhotographersInfo;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotographersInfoController : ControllerBase, IPhotographersInfoController
    {
        private readonly PhotographersInfoService _photographersInfoService;

        public PhotographersInfoController(DataContext dataContext)
        {
            _photographersInfoService = new PhotographersInfoService(dataContext);
        }

        [HttpGet("{photographerId}")]
        public async Task<ActionResult<PhotographerInfo>> GetPhotographerInfoByPhotographerId(int photographerId)
        {
            var photographerInfo = await _photographersInfoService.GetPhotographerInfoByPhotographerId(photographerId);

            return photographerInfo == null ? NotFound() : Ok(photographerInfo);
        }

        [HttpPut]
        public async Task<ActionResult<PhotographerInfo>> UpdatePhotographerInfo(UpdatePhotographerInfoDto updatedPhotographerInfo)
        {
            try
            {
                return await _photographersInfoService.UpdatePhotographerInfo(updatedPhotographerInfo);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
