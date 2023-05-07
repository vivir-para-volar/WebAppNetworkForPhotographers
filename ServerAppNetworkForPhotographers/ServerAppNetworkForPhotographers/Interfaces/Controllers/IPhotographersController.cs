using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotographersController
    {
        Task<ActionResult<Photographer?>> GetPhotographerById(int id);
        Task<ActionResult<Photographer>> CreatePhotographer(CreatePhotographerDto photographerDto);
        Task<ActionResult<Photographer>> UpdatePhotographer(UpdatePhotographerDto photographerDto);
        Task<ActionResult<Photographer>> UpdatePhotographerPhoto(int id, IFormFile photo);
        Task<ActionResult> DeletePhotographer(int id);
    }
}
