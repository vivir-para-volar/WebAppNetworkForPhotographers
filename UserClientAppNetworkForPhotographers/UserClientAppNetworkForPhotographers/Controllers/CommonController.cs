using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Lists;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult ApiError(int status, string message)
        {
            return View(new ApiException(status, message));
        }

        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> GetPhotographerPhoto(string name)
        {
            Stream photo;

            try
            {
                photo = await ApiPhotographers.GetPhotoByName(name, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }
            
            return File(photo, "image/jpeg");
        }

        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> GetBlogMainPhoto(string name)
        {
            Stream photo;

            try
            {
                photo = await ApiContents.GetBlogMainPhotoByName(name, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }
            
            return File(photo, "image/jpeg");
        }

        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> GetContentPhoto(int contentId, string name)
        {
            Stream photo;

            try
            {
                photo = await ApiPhotos.GetPhotoByName(contentId, name, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return File(photo, "image/jpeg");
        }
    }
}
