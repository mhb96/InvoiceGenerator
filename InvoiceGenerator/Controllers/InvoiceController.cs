using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Models;
using InvoiceGenerator.Services;
using InvoiceGenerator.Services.Models.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IFileHelper _fileHelper;

        public InvoiceController(IInvoiceService invoiceService, IFileHelper fileHelper)
        {
            _invoiceService = invoiceService;
            _fileHelper = fileHelper;
        }

        [Authorize]
        [HttpGet("/api/invoices/get")]
        public async Task<IActionResult> GetInvoices()
        {
            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

            var invoices = await _invoiceService.GetForDashboardAsync(userId);

            return Ok(new DashboardOutputViewModel { Invoices = invoices });
        }

        [Authorize]
        [HttpGet("/api/invoice/create")]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost("/api/invoice/create")]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceInputModel input)
        {
            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

            long invoiceId = await _invoiceService.CreateAsync(new CreateInvoiceModel
            {
                Address = input.Address,
                TotalFee = input.TotalFee,
                ClientName = input.ClientName,
                Comment = input.Comment,
                CompanyName = input.CompanyName,
                CreatedDate = input.CreatedDate,
                DueDate = input.DueDate,
                EmailAddress = input.EmailAddress,
                PhoneNumber = input.PhoneNumber,
                Vat = input.Vat,
                Items = input.Items,
                UserId = userId
            });

            return Ok(invoiceId);
        }

        [Authorize]
        [HttpGet("/invoice/view/{id}")]
        public async Task<IActionResult> View(long id)
        {
            return View(await _invoiceService.GetAsync(id, false));
        }

        [Authorize]
        [HttpPost("api/invoice/download/{id}")]
        public async Task<IActionResult> Download(long id)
        {
            return Ok(await _invoiceService.CreateInvoicePdf(id));
        }

        [Authorize]
        [HttpPost("api/invoice/deleteTemp")]
        public IActionResult DeleteTemp()
        {
            _fileHelper.DeleteAllTempFiles();
            return Ok();
        }
    }
}
