using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult ApiError(int status, string message)
        {
            return View(new ApiException(status, message));
        }

        public async Task<ActionResult> GetPhotographerPhoto(string name)
        {
            var photo = await ApiPhotographers.GetPhotoByName(name, AppUser.GetToken(HttpContext));
            return File(photo, "image/jpeg");
        }

        public async Task<ActionResult> GetContentPhoto(int contentId, string name)
        {
            var photo = await ApiPhotos.GetPhotoByName(contentId, name, AppUser.GetToken(HttpContext));
            return File(photo, "image/jpeg");
        }
    }
}
