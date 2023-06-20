using EmployeeClientAppNetworkForPhotographers.API.ApiRequests;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult ApiError(int status, string message)
        {
            return View(new ApiException(status, message));
        }

        [Authorize(Roles = UserRoles.AdminEmployee)]
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

        [Authorize(Roles = UserRoles.AdminEmployee)]
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

        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult> GetContentPhoto(int contentId, string name)
        {
            Stream photo;

            try
            {
                photo = await ApiContents.GetPhotoByName(contentId, name, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return File(photo, "image/jpeg");
        }

        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult> GetPhotoInfo(int photoId)
        {
            PhotoInfo photoInfo;

            try
            {
                photoInfo = await ApiContents.GetPhotoInfoByPhotoId(photoId, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, photoInfo);
        }
    }
}
