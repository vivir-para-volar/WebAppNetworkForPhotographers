using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.Exceptions;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    public class GeneralController : Controller
    {
        public ActionResult ApiError(int status, string message)
        {
            return View(new ApiException(status, message));
        }
    }
}
