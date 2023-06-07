using EmployeeClientAppNetworkForPhotographers.API.ApiRequests;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Complaints;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.AdminEmployee)]
    public class ComplaintsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<GetPhotographerCountComplaints> complaints;

            try
            {
                complaints = await ApiComplaints.GetCountPhotographersComplaintsOpen(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(complaints);
        }

        public async Task<ActionResult> Photographer(int id)
        {
            List<GetContentWithCountComplaints> complaints;
            GetPhotographerForListDto photographer;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                complaints = await ApiComplaints.GetPhotodrapherContentsWithCountComplaints(id, token);
                photographer = await ApiPhotographers.GetById(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            ViewBag.Photographer = photographer;
            return View(complaints);
        }

        public async Task<ActionResult> Content(int id)
        {
            List<Complaint> complaints;
            GetContentForEmployeeDto content;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                complaints = await ApiComplaints.GetComplaintsOpenForContent(id, token);
                content = await ApiContents.GetById(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            ViewBag.Content = content;
            return View(complaints);
        }

        public async Task<ActionResult> UpdateStatus(int id)
        {
            try
            {
                await ApiComplaints.UpdateStatus(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        public async Task<ActionResult> UpdateStatusAllForContent(int id, int photographerId)
        {
            try
            {
                await ApiComplaints.UpdateAllStatusForContent(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(Photographer), new { id = photographerId });
        }

        public async Task<ActionResult> UpdateStatusContent(int id, int photographerId)
        {
            try
            {
                await ApiContents.UpdateStatus(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(Photographer), new { id = photographerId });
        }

        public async Task<ActionResult> UpdateStatusPhotographer(int id)
        {
            try
            {
                await ApiPhotographers.UpdateStatus(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> ShowContent(int id)
        {
            GetContentForEmployeeDto content;

            try
            {
                content = await ApiContents.GetById(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            if (content.Type == TypeContent.Post)
            {
                return View("ContentPost", content);
            }
            else
            {
                return View("ContentBlog", content);
            }
        }
    }
}