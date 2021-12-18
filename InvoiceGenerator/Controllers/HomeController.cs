using InvoiceGenerator.Models;
using InvoiceGenerator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoiceGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public HomeController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "User");
            return View();
        }

        [Authorize]
        [HttpGet("/api/get/invoices")]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _invoiceService.GetForDashboardAsync();
            return Ok(new DashboardOutputViewModel { Invoices = invoices });
        }
    }
}
