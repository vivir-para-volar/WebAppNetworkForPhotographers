using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using UserClientAppNetworkForPhotographers.Models.Lists;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Photographers(string data, int part)
        {
            List<GetPhotographerForListDto> photographers;

            try
            {
                photographers = await ApiSearch.Photographers(new SearchDto(data), part, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, photographers);
        }

        public async Task<ActionResult> Posts(string data, int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiSearch.Posts(new SearchDto(data), part, AppUser.GetToken(HttpContext));
                contents.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }

        public async Task<ActionResult> Blogs(string data, int part)
        {
            List<GetContentForListDto> contents;

            try
            {
                var userId = AppUser.GetPhotographerId(HttpContext);

                contents = await ApiSearch.Blogs(new SearchDto(data), part, AppUser.GetToken(HttpContext));
                contents.ForEach(item => item.AppUserId = userId);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, contents);
        }
    }
}
