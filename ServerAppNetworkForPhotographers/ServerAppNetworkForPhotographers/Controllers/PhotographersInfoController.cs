using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Dtos.PhotographersInfo;
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
        public async Task<ActionResult<PhotographerInfo?>> GetPhotographerInfoByPhotographerId(int photographerId)
        {
            return Ok(await _photographersInfoService.GetPhotographerInfoByPhotographerId(photographerId));
        }

        [HttpPut]
        public async Task<ActionResult<PhotographerInfo>> UpdatePhotographerInfo(UpdatePhotographerInfoDto photographerInfoDto)
        {
            try
            {
                return Ok(await _photographersInfoService.UpdatePhotographerInfo(photographerInfoDto));
            }
            catch (PhotographerInfoNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
