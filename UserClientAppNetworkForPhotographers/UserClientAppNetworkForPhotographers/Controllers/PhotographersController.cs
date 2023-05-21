using Microsoft.AspNetCore.Mvc;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    public class PhotographersController : Controller
    {
        public ActionResult Settings()
        {
            return View();
        }
    }
}
