﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Lists;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Account;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class SettingsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> UpdateProfile()
        {
            Photographer photographer;
            try
            {
                var token = AppUser.GetToken(HttpContext);

                photographer = await ApiPhotographer.GetById(AppUser.GetPhotographerId(HttpContext), token);
                photographer.PhotographerInfo = await ApiPhotographer.GetInfoByPhotographerId(photographer.Id, token);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return View(new PhotographerWithInfoDto(photographer));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProfilePhoto(IFormFile photo, PhotographerWithInfoDto photographer)
        {
            if (photo == null)
            {
                ModelState.AddModelError(nameof(Photographer.PhotoProfile), "Выберите фото");
                return View(nameof(UpdateProfile), photographer);
            }

            try
            {
                await ApiPhotographer.UpdatePhotoProfile(AppUser.GetPhotographerId(HttpContext), photo, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePhotographer(PhotographerWithInfoDto photographer)
        {
            if (!ModelState.IsValid) return View(nameof(UpdateProfile), photographer);

            var updatePhotographer = new UpdatePhotographerDto(photographer);

            try
            {
                await ApiPhotographer.Update(updatePhotographer, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePhotographerInfo(PhotographerWithInfoDto photographer)
        {
            if (!ModelState.IsValid) return View(nameof(UpdateProfile), photographer);

            var updatePhotographerInfo = new UpdatePhotographerInfoDto(photographer);

            try
            {
                await ApiPhotographer.UpdateInfo(updatePhotographerInfo, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
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

            updatePassword.PhotographerId = AppUser.GetPhotographerId(HttpContext);

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
                return StatusCode(ex.Status, ex.Message);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        public async Task<ActionResult> DeleteAccount()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            try
            {
                await ApiPhotographer.Delete(AppUser.GetPhotographerId(HttpContext), AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
    }
}
