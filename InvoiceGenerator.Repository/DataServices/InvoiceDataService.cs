using InvoiceGenerator.Common.DataTypes;
using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Models.Invoice;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using InvoiceGenerator.Repository.Models.Invoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices
{
    /// <summary>
    /// The invoice data service class.
    /// </summary>
    public class InvoiceDataService : BaseDataService, IInvoiceDataService
    {
        public InvoiceDataService(IUnitOfWork unitOfWork, ILogger<InvoiceDataService> logger) : base(unitOfWork, logger)
        { }

        ///<inheritdoc/>
        public async Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId) =>
           await UnitOfWork.Query<Invoice>(i => i.UserId == userId).Select(i => new InvoiceSummaryModel
           {
               CreatedDate = i.CreatedAt.ToString("dd/MM/yyyy"),
               DueDate = i.DueDate.ToString("dd/MM/yyyy"),
               InvoiceNo = i.Id.ToString("D6"),
               ToCompany = i.ClientCompanyName,
               TotalFee = $"{i.Currency.Code} {i.TotalFee.ToString("F2")}",
               PaymentStatus = i.FeePaid == 0 ? PaymentStatus.unpaid : i.FeePaid < i.TotalFee ? PaymentStatus.partial : PaymentStatus.full
           }).ToListAsync();

        ///<inheritdoc/>
        public async Task<InvoiceModel> GetAsync(long invoiceId)
        {
            return await UnitOfWork.Query<Invoice>(i => i.Id == invoiceId).Select(i => new InvoiceModel
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
                FeePaid = i.FeePaid,
                UserAddress = i.UserAddress,
                UserCompanyName = i.UserCompanyName,
                UserContactNo = i.UserContactNo,
                UserEmail = i.UserEmail,
                CurrencyCode = i.Currency.Code,
                CurrencyId = i.Currency.Id,
                UserCompanyLogoId = i.UserCompanyLogoId,
            }).FirstOrDefaultAsync();
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(long invoiceId, long userId)
        {
            var invoice = await UnitOfWork.FirstOrDefaultAsync<Invoice>(i => i.Id == invoiceId && i.UserId == userId);
            if (invoice is null)
            {
                Logger.LogError($"Invoice with id: {invoiceId} was not found.");
                throw new IGException("Invoice does not exist.");
            }

            Logger.LogInformation($"Attempting to delete invoice: {invoice.Id} {invoice.CreatedAt.Date}, {invoice.ClientName}, {invoice.TotalFee}");
            invoice.IsDeleted = true;
            UnitOfWork.Update(invoice);
            await UnitOfWork.SaveAsync();
            Logger.LogInformation($"Successfully deleted invoice: {invoiceId}");
        }

        ///<inheritdoc/>
        public async Task EditAsync(long invoiceId, long userId, EditInvoiceDataModel editInvoiceModel)
        {
            var invoice = await UnitOfWork.FirstOrDefaultAsync<Invoice>(i => i.Id == invoiceId && i.UserId == userId);
            if (invoice is null)
            {
                Logger.LogError($"Invoice with id: {invoiceId} was not found.");
                throw new IGException("Invoice does not exist.");
            }

            invoice.CreatedDate = editInvoiceModel.CreatedDate;
            invoice.DueDate = editInvoiceModel.DueDate;
            invoice.UserAddress = editInvoiceModel.UserAddress;
            invoice.UserCompanyName = editInvoiceModel.UserCompanyName;
            invoice.UserContactNo = editInvoiceModel.UserContactNo;
            invoice.UserEmail = editInvoiceModel.UserEmail;
            invoice.ClientName = editInvoiceModel.ClientName;
            invoice.ClientAddress = editInvoiceModel.ClientAddress;
            invoice.ClientCompanyName = editInvoiceModel.ClientCompanyName;
            invoice.ClientEmailAddress = editInvoiceModel.ClientEmailAddress;
            invoice.ClientPhoneNumber = editInvoiceModel.ClientPhoneNumber;
            invoice.TotalFee = editInvoiceModel.TotalFee;
            invoice.FeePaid = editInvoiceModel.FeePaid;
            invoice.Vat = editInvoiceModel.Vat;
            invoice.CurrencyId = editInvoiceModel.CurrencyId;
            invoice.Comment = editInvoiceModel.Comment;

            UnitOfWork.Update(invoice);
            await UnitOfWork.SaveAsync();
        }

        ///<inheritdoc/>
        public async Task<long> CreateAsync(CreateInvoiceDataModel createInvoiceModel)
        {
            Logger.LogInformation($"Creating new invoice.");

            Invoice invoice = new()
            {
                UserId = createInvoiceModel.UserId,
                UserAddress = createInvoiceModel.UserAddress,
                UserCompanyName = createInvoiceModel.UserCompanyName,
                UserContactNo = createInvoiceModel.UserContactNo,
                UserEmail = createInvoiceModel.UserEmail,
                UserCompanyLogoId = createInvoiceModel.UserCompanyLogoId,
                ClientAddress = createInvoiceModel.ClientAddress,
                ClientName = createInvoiceModel.ClientName,
                Comment = createInvoiceModel.Comment,
                ClientCompanyName = createInvoiceModel.ClientCompanyName,
                CreatedDate = createInvoiceModel.CreatedDate,
                DueDate = createInvoiceModel.DueDate,
                CreatedAt = DateTime.Now,
                ClientEmailAddress = createInvoiceModel.ClientEmailAddress,
                ClientPhoneNumber = createInvoiceModel.ClientPhoneNumber,
                TotalFee = createInvoiceModel.TotalFee,
                Vat = createInvoiceModel.Vat,
                CurrencyId = createInvoiceModel.CurrencyId,
                FeePaid = createInvoiceModel.FeePaid,
                IsDeleted = false
            };

            await UnitOfWork.AddAsync(invoice);
            await UnitOfWork.SaveAsync();

            return invoice.Id;
        }
    }
}
