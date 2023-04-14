using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotographersController
    {
        Task<ActionResult<Photographer>> GetPhotographerById(int id);
        Task<ActionResult<Photographer>> CreatePhotographer(CreatePhotographerDto newPhotographer);
        Task<ActionResult<Photographer>> UpdatePhotographer(UpdatePhotographerDto updatedPhotographer);
        Task<ActionResult<Photographer>> UpdatePhotographerProfilePhoto(int id);
        Task<ActionResult<Photographer>> UpdatePhotographerLastLoginDate(int id);
        Task<ActionResult> DeletePhotographer(int id);
    }
}
