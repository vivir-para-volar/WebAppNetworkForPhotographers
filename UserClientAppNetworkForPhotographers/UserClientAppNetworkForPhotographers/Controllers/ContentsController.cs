using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.Models.Lists;
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
                if (post.Type != TypeContent.Post)
                {
                    return RedirectToAction(nameof(CommonController.ApiError), "General", new { status = StatusCodes.Status400BadRequest });
                }

                post.Comments = await ApiComments.GetAllForContent(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "General", ex.ToObj());
            }

            post.UserId = AppUser.GetPhotographerId(HttpContext);

            return View(post);
        }

        public ActionResult Blog(int id)
        {
            return View();
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        public ActionResult CreateBlog()
        {
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ApiContents.Delete(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "General", ex.ToObj());
            }

            return RedirectToAction(nameof(ProfilesController.Index), "Profiles");
        }
    }
}
