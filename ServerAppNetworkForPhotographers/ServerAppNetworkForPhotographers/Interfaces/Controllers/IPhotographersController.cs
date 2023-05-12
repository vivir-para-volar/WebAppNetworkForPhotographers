using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotographersController
    {
        Task<ActionResult<Photographer?>> GetPhotographerById(int id);
        Task<ActionResult<List<GetPhotographerForListDto>>> SearchPhotographers(SearchPhotographerDto searchPhotographerDto);
        Task<ActionResult<Photographer>> CreatePhotographer(CreatePhotographerDto photographerDto);
        Task<ActionResult<Photographer>> UpdatePhotographer(UpdatePhotographerDto photographerDto);
        Task<ActionResult<string>> UpdatePhotographerPhoto(int id, IFormFile photo);
        Task<ActionResult> DeletePhotographer(int id);
    }
}
