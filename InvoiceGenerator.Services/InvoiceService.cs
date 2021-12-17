using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository;
using InvoiceGenerator.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        public InvoiceService(IUnitOfWork unitOfWork, ILogger<InvoiceService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<List<InvoiceSummaryModel>> GetForDashboardAsync() =>
            await UnitOfWork.Query<Invoice>().Select(i => new InvoiceSummaryModel { Date = i.CreatedAt.Date, InvoiceNo = i.Id, ToCompany = i.ClientName, TotalFee = i.TotalFee }).ToListAsync();

        public async Task DeleteAsync(long id)
        {
            var invoice = await UnitOfWork.FirstOrDefaultAsync<Invoice>(i => i.Id == id);
            Logger.LogInformation($"Attempting to delete invoice: {invoice.Id} {invoice.CreatedAt.Date}, {invoice.ClientName}, {invoice.TotalFee}");
            invoice.IsDeleted = true;
            UnitOfWork.Update(invoice);
            await UnitOfWork.SaveAsync();
            Logger.LogInformation($"Successfully deleted invoice: {id}");
        }
    }
}