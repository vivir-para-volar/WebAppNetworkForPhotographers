using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Lists;

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

            post.AppUserId = AppUser.GetPhotographerId(HttpContext);

            return View(post);
        }

        public ActionResult Blog(int id)
        {
            return View();
        }

        public async Task<ActionResult> CreatePost()
        {
            var contentPostDto = new CreateContentPostDto();
            contentPostDto.PhotographerId = AppUser.GetPhotographerId(HttpContext);

            try
            {
                contentPostDto.CategoryDirs = await ApiCategories.GetAllWithDirs(AppUser.GetToken(HttpContext));
                contentPostDto.CategoryDirsJson = JsonConvert.SerializeObject(contentPostDto.CategoryDirs);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(contentPostDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost(List<IFormFile> photos, CreateContentPostDto contentPostDto)
        {
            if (!ModelState.IsValid)
            {
                contentPostDto.CategoryDirs = JsonConvert.DeserializeObject<List<GetCategoryDirDto>>(contentPostDto.CategoryDirsJson);
                return View(contentPostDto);
            }

            if (photos.Count == 0)
            {
                contentPostDto.CategoryDirs = JsonConvert.DeserializeObject<List<GetCategoryDirDto>>(contentPostDto.CategoryDirsJson);

                ModelState.AddModelError("", "Выберите фото");
                return View(contentPostDto);
            }

            Content? post = null;
            try
            {
                post = await ApiContents.CreatePost(contentPostDto, AppUser.GetToken(HttpContext));

                foreach (var photo in photos)
                {
                    await ApiPhotos.Create(post.Id, photo, AppUser.GetToken(HttpContext));
                }
            }
            catch (ApiException ex)
            {
                for (var i = 10; post != null && i != 0; i--)
                {
                    try
                    {
                        await ApiContents.Delete(post.Id, AppUser.GetToken(HttpContext));
                        post = null;
                    }
                    catch (ApiException) { }
                }

                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(ProfilesController.Index), "Profiles");
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
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(ProfilesController.Index), "Profiles");
        }
    }
}
