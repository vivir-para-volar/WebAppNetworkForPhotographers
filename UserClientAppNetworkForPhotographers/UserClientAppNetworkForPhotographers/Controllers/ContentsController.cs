using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Lists;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class ContentsController : Controller
    {
        public async Task<ActionResult> Post(int id)
        {
            GetContentDto post;

            try
            {
                var token = AppUser.GetToken(HttpContext);

                post = await ApiContents.GetById(id, token);
                post.Comments = await ApiComments.GetAllForContent(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(GeneralController.ApiError), "General", ex.ToObj());
            }

            if (post.Type != TypeContent.Post)
            {
                return RedirectToAction(nameof(GeneralController.ApiError), "General", new { status = StatusCodes.Status400BadRequest });
            }

            post.UserId = AppUser.GetPhotographerId(HttpContext);

            return View(post);
        }

        public ActionResult Blog(int id)
        {
            return View();
        }
    }
}
