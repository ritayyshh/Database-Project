using Microsoft.AspNetCore.Mvc;

namespace OnlineJobPortal.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
