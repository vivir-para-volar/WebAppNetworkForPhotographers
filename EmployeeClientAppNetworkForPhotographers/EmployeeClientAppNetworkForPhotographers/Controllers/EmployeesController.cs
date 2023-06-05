using EmployeeClientAppNetworkForPhotographers.API.ApiRequests;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Account;
using EmployeeClientAppNetworkForPhotographers.Models.Account.Dtos;
using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class EmployeesController : Controller
    {
        private struct EmployeeRole
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public EmployeeRole(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        private List<EmployeeRole> _roles;

        public EmployeesController()
        {
            _roles = new List<EmployeeRole>
            {
                new EmployeeRole(UserRoles.Admin, "Админ"),
                new EmployeeRole(UserRoles.Employee, "Сотрудник")
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
            ViewBag.RoleType = new SelectList(_roles, nameof(EmployeeRole.Id), nameof(EmployeeRole.Name));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleType = new SelectList(_roles, nameof(EmployeeRole.Id), nameof(EmployeeRole.Name));
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
                ViewBag.RoleType = new SelectList(_roles, nameof(EmployeeRole.Id), nameof(EmployeeRole.Name));
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

            ViewBag.RoleType = new SelectList(_roles, nameof(EmployeeRole.Id), nameof(EmployeeRole.Name));
            return View(appUser);
        }

        [HttpPost]
        public async Task<ActionResult> Update(AppUserDto appUserDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleType = new SelectList(_roles, nameof(EmployeeRole.Id), nameof(EmployeeRole.Name));
                return View(appUserDto);
            }

            try
            {
                appUserDto = await ApiAccount.UpdateAppUser(appUserDto, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                ViewBag.RoleType = new SelectList(_roles, nameof(EmployeeRole.Id), nameof(EmployeeRole.Name));
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

            return RedirectToAction("Index");
        }
    }
}
