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
        [HttpGet("/invoice/create")]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost("/api/invoice/create")]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceInputModel input)
        {
            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

            long invoiceId = await _invoiceService.CreateAsync(new CreateInvoiceModel
            {
                ClientAddress = input.Address,
                TotalFee = input.TotalFee,
                ClientName = input.ClientName,
                Comment = input.Comment,
                ClientCompanyName = input.CompanyName,
                CreatedDate = input.CreatedDate,
                DueDate = input.DueDate,
                ClientEmailAddress = input.EmailAddress,
                ClientPhoneNumber = input.PhoneNumber,
                Vat = input.Vat,
                Items = input.Items,
                UserId = userId,
                CurrencyId = input.CurrencyId,
                FeePaid = input.FeePaid,
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
        [HttpGet("/invoice/edit/{id}")]
        public IActionResult Edit(long id) => View(new EditInvoiceViewModel { InvoiceId = id });

        [Authorize]
        [HttpGet("/api/invoice/getInvoiceDetails/{id}")]
        public async Task<IActionResult> GetInvoiceDetails(long id) => Ok(await _invoiceService.GetAsync(id, false));

        [Authorize]
        [HttpPost("/api/invoice/edit/{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] EditInvoiceInputModel input)
        {
            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

            await _invoiceService.EditAsync(new EditInvoiceModel
            {
                InvoiceId = id,
                UserCompanyName = input.UserCompanyName,
                UserAddress = input.UserAddress,
                CurrencyId = input.CurrencyId,
                UserEmailAddress = input.UserEmail,
                UserPhoneNumber = input.UserContactNo,
                ClientAddress = input.Address,
                TotalFee = input.TotalFee,
                ClientName = input.ClientName,
                Comment = input.Comment,
                ClientCompanyName = input.CompanyName,
                CreatedDate = input.CreatedDate,
                DueDate = input.DueDate,
                ClientEmailAddress = input.EmailAddress,
                ClientPhoneNumber = input.PhoneNumber,
                Vat = input.Vat,
                Items = input.Items,
                UserId = userId,
                FeePaid = input.FeePaid
            });

            return Ok(id);
        }

        [Authorize]
        [HttpPost("/invoice/delete/{id}")]
        public IActionResult Delete(long id)
        {
            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            _invoiceService.DeleteAsync(invoiceId: id, userId: userId);
            return Ok();
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
