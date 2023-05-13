using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotographersInfo;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
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
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
