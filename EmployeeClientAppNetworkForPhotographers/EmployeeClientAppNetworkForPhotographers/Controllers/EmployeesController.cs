using EmployeeClientAppNetworkForPhotographers.API.ApiRequests;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Account;
using EmployeeClientAppNetworkForPhotographers.Models.Account.Dtos;
using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeClientAppNetworkForPhotographers.Models;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class EmployeesController : Controller
    {
        private List<ViewSelectList> _roles;

        public EmployeesController()
        {
            _roles = new List<ViewSelectList>
            {
                new ViewSelectList(UserRoles.Admin, "Админ"),
                new ViewSelectList(UserRoles.Employee, "Сотрудник")
            };
        }

        public async Task<ActionResult> Index()
        {
            List<AppUserDto> appUsers;

            try
            {
                appUsers = await ApiAccount.GetAllAppUsers(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(appUsers);
        }

        public ActionResult Create()
        {
            ViewBag.RoleType = new SelectList(_roles, nameof(ViewSelectList.ValueStr), nameof(ViewSelectList.Name));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleType = new SelectList(_roles, nameof(ViewSelectList.ValueStr), nameof(ViewSelectList.Name));
                return View(userRegister);
            }

            AppUserDto appUser;
            try
            {
                if (userRegister.Role == UserRoles.Admin)
                {
                    appUser = await ApiAccount.RegisterAdmin(userRegister, AppUser.GetToken(HttpContext));
                }
                else
                {
                    appUser = await ApiAccount.RegisterEmployee(userRegister, AppUser.GetToken(HttpContext));
                }
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                ViewBag.RoleType = new SelectList(_roles, nameof(ViewSelectList.ValueStr), nameof(ViewSelectList.Name));
                return View(userRegister);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("Details", appUser);
        }

        public async Task<ActionResult> Update(string id)
        {
            AppUserDto appUser;

            try
            {
                appUser = await ApiAccount.GetAppUserById(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            ViewBag.RoleType = new SelectList(_roles, nameof(ViewSelectList.ValueStr), nameof(ViewSelectList.Name));
            return View(appUser);
        }

        [HttpPost]
        public async Task<ActionResult> Update(AppUserDto appUserDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleType = new SelectList(_roles, nameof(ViewSelectList.ValueStr), nameof(ViewSelectList.Name));
                return View(appUserDto);
            }

            try
            {
                appUserDto = await ApiAccount.UpdateAppUser(appUserDto, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                ViewBag.RoleType = new SelectList(_roles, nameof(ViewSelectList.ValueStr), nameof(ViewSelectList.Name));
                return View(appUserDto);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("Details", appUserDto);
        }

        public async Task<ActionResult> Delete(string id)
        {
            AppUserDto appUser;

            try
            {
                appUser = await ApiAccount.GetAppUserById(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(appUser);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeletePost(string id)
        {
            try
            {
                await ApiAccount.DeleteAppUser(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            if (id == AppUser.GetUserId(HttpContext))
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            return RedirectToAction("Index");
        }
    }
}
