using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.AdminEmployee)]
    public class ComplaintsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}