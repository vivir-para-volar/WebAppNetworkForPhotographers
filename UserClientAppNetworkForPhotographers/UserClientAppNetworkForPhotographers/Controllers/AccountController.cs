using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Account;
using UserClientAppNetworkForPhotographers.Models.Account.Dtos;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            ModelState.AddModelError("", "Недостаточно прав");
            return View(nameof(Login));
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            try
            {
                await ApiAccount.Register(userRegister);
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(userRegister);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(Login));
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);

            TokenDto tokenDto;
            try
            {
                tokenDto = await ApiAccount.Login(userLogin);

            }
            catch (ApiException ex)
            {
                if (ex.Status == StatusCodes.Status404NotFound)
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View(userLogin);
                }
                else return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            await CreateCookieAuthentication(tokenDto);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }

        private async Task CreateCookieAuthentication(TokenDto tokenDto)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, tokenDto.Role),
                new Claim("Token", tokenDto.Token),
                new Claim("PhotographerId", tokenDto.UserId)
            });
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,
                ExpiresUtc = DateTimeOffset.Now.AddMonths(1),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);
        }
    }
}
