using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.Lists;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Lists;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var showHome = new ShowHomeDto();

            var userId = AppUser.GetPhotographerId(HttpContext);
            var token = AppUser.GetToken(HttpContext);

            try
            {
                showHome.News = await ApiContents.GetNews(new NewsDto(userId), 1, token);
                showHome.Others = await ApiContents.GetOthers(new OthersDto(TypeSorting.Top, TypeSorting.PeriodWeek), 1, token);
                showHome.CategoryDirs = await ApiCategories.GetAllWithDirs(token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            showHome.News.ForEach(item => item.AppUserId = userId);
            showHome.Others.ForEach(item => item.AppUserId = userId);

            return View(showHome);
        }

        public async Task<ActionResult> IndexWithCategory(int id)
        {
            var showHome = new ShowHomeDto();
            showHome.ChooseCategory = id;

            var userId = AppUser.GetPhotographerId(HttpContext);
            var token = AppUser.GetToken(HttpContext);

            try
            {
                showHome.News = await ApiContents.GetNews(new NewsDto(userId), 1, token);
                showHome.Others = await ApiContents.GetOthers(new OthersDto(TypeSorting.Top, TypeSorting.PeriodYear, id), 1, token);
                showHome.CategoryDirs = await ApiCategories.GetAllWithDirs(token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            showHome.News.ForEach(item => item.AppUserId = userId);
            showHome.Others.ForEach(item => item.AppUserId = userId);

            return View("Index", showHome);
        }

        [HttpPost]
        public async Task<ActionResult> GetNews(NewsDto newsDto, int part)
        {
            if (newsDto.CategoriesIds?.Length == 0) newsDto.CategoriesIds = null;

            List<GetContentForListDto> contents;

            var userId = AppUser.GetPhotographerId(HttpContext);
            newsDto.PhotographerId = userId;

            try
            {
                contents = await ApiContents.GetNews(newsDto, part, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            contents.ForEach(item => item.AppUserId = userId);

            return StatusCode(StatusCodes.Status200OK, contents);
        }

        [HttpPost]
        public async Task<ActionResult> GetOthers(OthersDto othersDto, int part)
        {
            if (othersDto.CategoriesIds?.Length == 0) othersDto.CategoriesIds = null;


            List<GetContentForListDto> contents;

            var userId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contents = await ApiContents.GetOthers(othersDto, part, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            contents.ForEach(item => item.AppUserId = userId);

            return StatusCode(StatusCodes.Status200OK, contents);
        }
    }
}