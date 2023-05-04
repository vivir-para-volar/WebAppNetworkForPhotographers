using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotographersController
    {
        Task<ActionResult<Photographer?>> GetPhotographerById(int id);
        Task<ActionResult<Photographer>> CreatePhotographer(CreatePhotographerDto photographerDto);
        Task<ActionResult<Photographer>> UpdatePhotographer(UpdatePhotographerDto photographerDto);
        Task<ActionResult<Photographer>> UpdatePhotographerPhoto(int id);
        Task<ActionResult> DeletePhotographer(int id);
    }
}
