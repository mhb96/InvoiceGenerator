using InvoiceGenerator.Common.Exception;
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
        public InvoiceService(IItemService itemService, IUnitOfWork unitOfWork, ILogger<InvoiceService> logger) : base(unitOfWork, logger)
        {
            _itemService = itemService;
        }

        public async Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId) =>
            await UnitOfWork.Query<Invoice>(i => i.UserId == userId).Select(i => new InvoiceSummaryModel
            {
                CreatedDate = i.CreatedAt.ToString("dd/MM/yyyy"),
                DueDate = i.DueDate.ToString("dd/MM/yyyy"),
                InvoiceNo = i.Id.ToString("D6"),
                ToCompany = i.CompanyName,
                TotalFee = i.TotalFee.ToString("F")
            }).ToListAsync();

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

            if (string.IsNullOrEmpty(input.CompanyName))
                throw new IGException("Required company name was not provided.");

            if (string.IsNullOrEmpty(input.CreatedDate))
                throw new IGException("Required creatred date does not exist.");

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
    }
}