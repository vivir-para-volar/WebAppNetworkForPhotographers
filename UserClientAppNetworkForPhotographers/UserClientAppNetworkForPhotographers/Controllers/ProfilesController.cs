using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Lists;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class ProfilesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            Photographer photographer;

            try
            {
                photographer = await ApiPhotographers.GetById(AppUser.GetPhotographerId(HttpContext), AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(photographer);
        }

        public async Task<ActionResult> Photographer(int id)
        {
            if (id == AppUser.GetPhotographerId(HttpContext))
            {
                return RedirectToAction(nameof(Index));
            }

            Photographer photographer;

            try
            {
                photographer = await ApiPhotographers.GetById(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(photographer);
        }
    }
}
