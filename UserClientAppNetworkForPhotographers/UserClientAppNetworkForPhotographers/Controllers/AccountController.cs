using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using System.Net;
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
            return RedirectToAction("Login");
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
                return StatusCode(ex.Status, ex.Message);
            }

            return RedirectToAction("Login");
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
                if (ex.Status == (int)HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View(userLogin);
                }
                else return StatusCode(ex.Status, ex.Message);
            }

            await CreateCookieAuthentication(tokenDto);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        private async Task CreateCookieAuthentication(TokenDto tokenDto)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, tokenDto.Role),
                new Claim("Token", tokenDto.Token),
                new Claim("PhotographerId", tokenDto.PhotographerId.ToString() ?? "-1")
            });
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,
                ExpiresUtc = DateTimeOffset.Now.AddHours(3),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);
        }
    }
}
