using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Helpers;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Common.Models.Invoice;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository;
using InvoiceGenerator.Services.Models;
using InvoiceGenerator.Services.Models.Invoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        private readonly IItemService _itemService;
        private readonly IFileHelper _fileHelper;
        public InvoiceService(IItemService itemService, IUnitOfWork unitOfWork, ILogger<InvoiceService> logger, IFileHelper fileHelper) : base(unitOfWork, logger)
        {
            _itemService = itemService;
            _fileHelper = fileHelper;
        }

        public async Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId) =>
            await UnitOfWork.Query<Invoice>(i => i.UserId == userId).Select(i => new InvoiceSummaryModel
            {
                CreatedDate = i.CreatedAt.ToString("dd/MM/yyyy"),
                DueDate = i.DueDate.ToString("dd/MM/yyyy"),
                InvoiceNo = i.Id.ToString("D6"),
                ToCompany = i.CompanyName,
                TotalFee = i.TotalFee.ToString("F2")
            }).ToListAsync();

        public async Task<InvoiceModel> GetAsync(long invoiceId, bool isForPdf)
        {
            var invoice = await UnitOfWork.Query<Invoice>(i => i.Id == invoiceId).Select(i => new InvoiceModel
            {
                InvoiceNo = i.Id,
                ClientAddress = i.Address,
                ClientCompanyName = i.CompanyName,
                ClientEmailAddress = i.EmailAddress,
                ClientName = i.ClientName,
                ClientPhoneNumber = i.PhoneNumber,
                Comment = i.Comment,
                CreatedDate = i.CreatedDate.ToString("dd/MM/yyyy"),
                DueDate = i.DueDate.ToString("dd/MM/yyyy"),
                Vat = i.Vat,
                UserAddress = i.User.Address,
                UserCompanyName = i.User.CompanyName,
                UserContactNo = i.User.ContactNo,
                UserEmail = i.User.Email,
                UserLogo = i.User.CompanyLogo != null ? isForPdf ? _fileHelper.GetImageAddress(i.User.CompanyLogo.ImageName, true) : _fileHelper.GetImageAddress(i.User.CompanyLogo.ImageName, false) : null
            }).FirstOrDefaultAsync();

            var items = await _itemService.GetAsync(invoiceId);

            decimal subTotal = 0M;
            foreach (var item in items)
                subTotal += item.Quantity * item.UnitPrice;

            decimal totalFee = Math.Round(subTotal + (subTotal * invoice.Vat / 100), 2, MidpointRounding.AwayFromZero);
            subTotal = Math.Round(subTotal, 2, MidpointRounding.AwayFromZero);

            invoice.TotalFee = totalFee.ToString("F2");
            invoice.SubTotalFee = subTotal.ToString("F2");
            invoice.Items = items;

            return invoice;
        }

        public async Task DeleteAsync(long id)
        {
            var invoice = await UnitOfWork.FirstOrDefaultAsync<Invoice>(i => i.Id == id);
            Logger.LogInformation($"Attempting to delete invoice: {invoice.Id} {invoice.CreatedAt.Date}, {invoice.ClientName}, {invoice.TotalFee}");
            invoice.IsDeleted = true;
            UnitOfWork.Update(invoice);
            await UnitOfWork.SaveAsync();
            Logger.LogInformation($"Successfully deleted invoice: {id}");
        }

        public async Task<long> CreateAsync(CreateInvoiceModel input)
        {
            if (input.Items?.Count == 0)
                throw new IGException("No items exist in this invoice!");

            if (input.Items?.Count > 15)
                throw new IGException("Number of items cannot be greater than 15!");

            if (string.IsNullOrEmpty(input.CompanyName))
                throw new IGException("Required company name was not provided.");

            if (string.IsNullOrEmpty(input.CreatedDate))
                throw new IGException("Required created date does not exist.");

            if (string.IsNullOrEmpty(input.DueDate))
                throw new IGException("Required due date does not exist.");

            if (DateTime.Parse(input.CreatedDate) > DateTime.Parse(input.DueDate))
                throw new IGException("Created date cannot be greater than Due date.");

            Logger.LogInformation($"Validating total price.");

            decimal subTotal = 0M;
            foreach (var item in input.Items)
                subTotal += item.Quantity * item.UnitPrice;

            decimal totalFee = Math.Round(subTotal + (subTotal * input.Vat / 100), 2, MidpointRounding.AwayFromZero);
            if (totalFee != input.TotalFee)
                throw new IGException("Total fee is not equal to total price of items plus vat.");

            Logger.LogInformation($"Creating new invoice.");

            Invoice invoice = new()
            {
                UserId = input.UserId,
                Address = input.Address,
                ClientName = input.ClientName,
                Comment = input.Comment,
                CompanyName = input.CompanyName,
                CreatedDate = DateTime.Parse(input.CreatedDate),
                DueDate = DateTime.Parse(input.DueDate),
                CreatedAt = DateTime.Now,
                EmailAddress = input.EmailAddress,
                PhoneNumber = input.PhoneNumber,
                TotalFee = totalFee,
                Vat = input.Vat,
                IsDeleted = false
            };

            await UnitOfWork.AddAsync(invoice);
            await UnitOfWork.SaveAsync();
            Logger.LogInformation($"Added new invoice: {invoice.Id}.");

            await _itemService.AddAsync(input.Items, invoice.Id);

            return invoice.Id;
        }

        public async Task<string> CreateInvoicePdf(long invoiceId)
        {
            InvoiceModel invoice = await GetAsync(invoiceId, true);
            var invoiceHtml = TemplateHelper.BuildInvoiceHtml(invoice);
            var fileName = _fileHelper.CreatePdf(invoiceHtml);
            return fileName;
        }
    }
}