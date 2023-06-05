using EmployeeClientAppNetworkForPhotographers.API.ApiRequests;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Account;
using EmployeeClientAppNetworkForPhotographers.Models.Account.Dtos;
using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.AdminEmployee)]
    public class ProfilesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            AppUserDto appUser;

            try
            {
                appUser = await ApiAccount.GetAppUserById(AppUser.GetUserId(HttpContext), AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(appUser);
        }

        public async Task<ActionResult> Update()
        {
            AppUserDto appUser;

            try
            {
                appUser = await ApiAccount.GetAppUserById(AppUser.GetUserId(HttpContext), AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(appUser);
        }

        [HttpPost]
        public async Task<ActionResult> Update(AppUserDto appUserDto)
        {
            if (!ModelState.IsValid) return View(appUserDto);

            try
            {
                appUserDto = await ApiAccount.UpdateAppUser(appUserDto, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(appUserDto);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(UpdatePassword updatePassword)
        {
            if (!ModelState.IsValid) return View(updatePassword);

            updatePassword.Id = AppUser.GetUserId(HttpContext);

            try
            {
                await ApiAccount.UpdatePassword(updatePassword, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, "Неверный пароль");
                return View(updatePassword);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
    }
}
