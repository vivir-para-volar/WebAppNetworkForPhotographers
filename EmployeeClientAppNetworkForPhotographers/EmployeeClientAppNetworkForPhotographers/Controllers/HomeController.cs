using Microsoft.AspNetCore.Mvc;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
    }
}