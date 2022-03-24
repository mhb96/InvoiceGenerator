using Microsoft.AspNetCore.Mvc;

namespace InvoiceGenerator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "User");
            return View();
        }

        [HttpGet]
        public IActionResult Help()
        {
            return View();
        }
    }
}
