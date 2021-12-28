using InvoiceGenerator.Models;
using InvoiceGenerator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoiceGenerator.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [Authorize]
        [HttpGet("/api/invoices/get")]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _invoiceService.GetForDashboardAsync();
            return Ok(new DashboardOutputViewModel { Invoices = invoices });
        }

        [Authorize]
        [HttpGet("/api/invoice/create")]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost("/api/invoice/create")]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceInputModel input)
        {
            await _invoiceService.GetForDashboardAsync();
            return Ok();
        }
    }
}
