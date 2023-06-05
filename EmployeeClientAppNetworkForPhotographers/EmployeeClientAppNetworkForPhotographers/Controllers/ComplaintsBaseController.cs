using EmployeeClientAppNetworkForPhotographers.API.ApiRequests;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase;
using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ComplaintsBaseController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<ComplaintBase> complaintsBase;

            try
            {
                complaintsBase = await ApiComplaintsBase.GetAll(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(complaintsBase);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateComplaintBaseDto complaintBaseDto)
        {
            if (!ModelState.IsValid) return View(complaintBaseDto);

            ComplaintBase complaintBase;
            try
            {
                complaintBase = await ApiComplaintsBase.Create(complaintBaseDto, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(complaintBaseDto);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("Details", complaintBase);
        }

        public async Task<ActionResult> Update(int id)
        {
            ComplaintBase complaintBase;

            try
            {
                complaintBase = await ApiComplaintsBase.GetById(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(complaintBase);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ComplaintBase complaintBase)
        {
            if (!ModelState.IsValid) return View(complaintBase);

            try
            {
                complaintBase = await ApiComplaintsBase.Update(complaintBase, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(complaintBase);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("Details", complaintBase);
        }

        public async Task<ActionResult> Delete(int id)
        {
            ComplaintBase complaintBase;
            bool checkComplaints;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                complaintBase = await ApiComplaintsBase.GetById(id, token);
                checkComplaints = await ApiComplaintsBase.CheckComplaints(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            ViewBag.CheckComplaints = checkComplaints;
            return View(complaintBase);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                await ApiComplaintsBase.Delete(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction("Index");
        }
    }
}
