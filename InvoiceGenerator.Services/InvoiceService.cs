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
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        public InvoiceService(IItemService itemService, IUnitOfWork unitOfWork, ILogger<InvoiceService> logger, IFileHelper fileHelper, IUserService userService, IImageService imageService) : base(unitOfWork, logger)
        {
            _itemService = itemService;
            _fileHelper = fileHelper;
            _userService = userService;
            _imageService = imageService;
        }

        public async Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId) =>
            await UnitOfWork.Query<Invoice>(i => i.UserId == userId).Select(i => new InvoiceSummaryModel
            {
                CreatedDate = i.CreatedAt.ToString("dd/MM/yyyy"),
                DueDate = i.DueDate.ToString("dd/MM/yyyy"),
                InvoiceNo = i.Id.ToString("D6"),
                ToCompany = i.ClientCompanyName,
                TotalFee = $"{i.Currency.Code} {i.TotalFee.ToString("F2")}"
            }).ToListAsync();

        public async Task<InvoiceModel> GetAsync(long invoiceId, bool isForPdf)
        {
            var invoice = await UnitOfWork.Query<Invoice>(i => i.Id == invoiceId).Select(i => new InvoiceModel
            {
                InvoiceNo = i.Id.ToString("D6"),
                ClientAddress = i.ClientAddress,
                ClientCompanyName = i.ClientCompanyName,
                ClientEmailAddress = i.ClientEmailAddress,
                ClientName = i.ClientName,
                ClientPhoneNumber = i.ClientPhoneNumber,
                Comment = i.Comment,
                CreatedDate = i.CreatedDate.ToString("dd/MM/yyyy"),
                DueDate = i.DueDate.ToString("dd/MM/yyyy"),
                Vat = i.Vat,
                UserAddress = i.UserAddress,
                UserCompanyName = i.UserCompanyName,
                UserContactNo = i.UserContactNo,
                UserEmail = i.UserEmail,
                CurrencyCode = i.Currency.Code,
                CurrencyId = i.Currency.Id,
                UserCompanyLogoId = i.UserCompanyLogoId
            }).FirstOrDefaultAsync();

            var image = await _imageService.GetAsync(invoice.UserCompanyLogoId);
            invoice.UserCompanyLogo = invoice.UserCompanyLogoId != 0 ? isForPdf ? _fileHelper.GetImageAddress(image.ImageName, true) : _fileHelper.GetImageAddress(image.ImageName, false) : null;

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

        public async Task DeleteAsync(long invoiceId, long userId)
        {
            var invoice = await UnitOfWork.FirstOrDefaultAsync<Invoice>(i => i.Id == invoiceId && i.UserId == userId );
            Logger.LogInformation($"Attempting to delete invoice: {invoice.Id} {invoice.CreatedAt.Date}, {invoice.ClientName}, {invoice.TotalFee}");
            invoice.IsDeleted = true;
            UnitOfWork.Update(invoice);
            await UnitOfWork.SaveAsync();
            Logger.LogInformation($"Successfully deleted invoice: {invoiceId}");
        }

        public async Task<long> EditAsync(EditInvoiceModel input)
        {
            if (string.IsNullOrEmpty(input.UserCompanyName))
                throw new IGException("Your company name cannot be empty!");

            if (string.IsNullOrEmpty(input.UserEmailAddress))
                throw new IGException("Your company email cannot be empty!");

            if (string.IsNullOrEmpty(input.UserAddress))
                throw new IGException("Your company address cannot be empty!");

            if (string.IsNullOrEmpty(input.UserPhoneNumber))
                throw new IGException("Your company phone number cannot be empty!");

            if (input.Items?.Count == 0)
                throw new IGException("No items exist in this invoice!");

            if (input.Items?.Count > 15)
                throw new IGException("Number of items cannot be greater than 15!");

            if (string.IsNullOrEmpty(input.ClientCompanyName))
                throw new IGException("Required client's company name was not provided.");

            if (string.IsNullOrEmpty(input.CreatedDate))
                throw new IGException("Required created date does not exist.");

            if (string.IsNullOrEmpty(input.DueDate))
                throw new IGException("Required due date does not exist.");

            if (DateTime.Parse(input.CreatedDate) > DateTime.Parse(input.DueDate))
                throw new IGException("Created date cannot be greater than Due date.");

            if (input.CurrencyId == 0)
                throw new IGException("No currency was selected");

            Logger.LogInformation($"Validating total price.");

            decimal subTotal = 0M;
            foreach (var item in input.Items)
                subTotal += item.Quantity * item.UnitPrice;

            decimal totalFee = Math.Round(subTotal + (subTotal * input.Vat / 100), 2, MidpointRounding.AwayFromZero);
            if (totalFee != input.TotalFee)
                throw new IGException("Total fee is not equal to total price of items plus vat.");

            Invoice invoice = await UnitOfWork.Query<Invoice>(i => i.Id == input.InvoiceId && i.UserId == input.UserId).FirstOrDefaultAsync();
            
            if (invoice == null)
                throw new IGException("Invoice does not exist!");

            Logger.LogInformation($"Updating invoice {invoice.Id}.");
                        
            invoice.UserAddress = input.UserAddress;
            invoice.UserCompanyName = input.UserCompanyName;
            invoice.UserContactNo = input.UserPhoneNumber;
            invoice.UserEmail = input.UserEmailAddress;
            invoice.ClientAddress = input.ClientAddress;
            invoice.ClientName = input.ClientName;
            invoice.Comment = input.Comment;
            invoice.ClientCompanyName = input.ClientCompanyName;
            invoice.CreatedDate = DateTime.Parse(input.CreatedDate);
            invoice.DueDate = DateTime.Parse(input.DueDate);
            invoice.ClientEmailAddress = input.ClientEmailAddress;
            invoice.ClientPhoneNumber = input.ClientPhoneNumber;
            invoice.TotalFee = totalFee;
            invoice.Vat = input.Vat;
            invoice.CurrencyId = input.CurrencyId;

            UnitOfWork.Update(invoice);
            await UnitOfWork.SaveAsync();

            await _itemService.DeleteAllAsync(invoice.Id);
            await _itemService.AddAsync(input.Items, invoice.Id);

            return invoice.Id;
        }

        public async Task<long> CreateAsync(CreateInvoiceModel input)
        {
            if (input.Items?.Count == 0)
                throw new IGException("No items exist in this invoice!");

            if (input.Items?.Count > 15)
                throw new IGException("Number of items cannot be greater than 15!");

            if (string.IsNullOrEmpty(input.ClientCompanyName))
                throw new IGException("Required company name was not provided.");

            if (string.IsNullOrEmpty(input.CreatedDate))
                throw new IGException("Required created date does not exist.");

            if (string.IsNullOrEmpty(input.DueDate))
                throw new IGException("Required due date does not exist.");

            if (DateTime.Parse(input.CreatedDate) > DateTime.Parse(input.DueDate))
                throw new IGException("Created date cannot be greater than Due date.");

            if (input.CurrencyId == 0)
                throw new IGException("No currency was selected");

            Logger.LogInformation($"Validating total price.");

            decimal subTotal = 0M;
            foreach (var item in input.Items)
                subTotal += item.Quantity * item.UnitPrice;

            decimal totalFee = Math.Round(subTotal + (subTotal * input.Vat / 100), 2, MidpointRounding.AwayFromZero);
            if (totalFee != input.TotalFee)
                throw new IGException("Total fee is not equal to total price of items plus vat.");

            var user = await _userService.GetAsync(input.UserId);

            Logger.LogInformation($"Creating new invoice.");

            Invoice invoice = new()
            {
                UserId = input.UserId,
                UserAddress = user.Address,
                UserCompanyName = user.CompanyName,
                UserContactNo = user.ContactNo,
                UserEmail = user.Email,
                UserCompanyLogoId = user.CompanyLogoId,
                ClientAddress = input.ClientAddress,
                ClientName = input.ClientName,
                Comment = input.Comment,
                ClientCompanyName = input.ClientCompanyName,
                CreatedDate = DateTime.Parse(input.CreatedDate),
                DueDate = DateTime.Parse(input.DueDate),
                CreatedAt = DateTime.Now,
                ClientEmailAddress = input.ClientEmailAddress,
                ClientPhoneNumber = input.ClientPhoneNumber,
                TotalFee = totalFee,
                Vat = input.Vat,
                CurrencyId = input.CurrencyId,
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