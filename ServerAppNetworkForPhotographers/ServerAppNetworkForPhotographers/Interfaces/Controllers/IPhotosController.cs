using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotosController
    {
        Task<ActionResult<List<Photo>>> CreatePhotos(int contentId, List<IFormFile> photos);
    }
}
