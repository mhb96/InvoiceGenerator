using InvoiceGenerator.Services.Models;
using InvoiceGenerator.Services.Models.Invoice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IInvoiceService : IBaseService
    {
        public Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId);
        public Task DeleteAsync(long id);
        public Task<long> CreateAsync(CreateInvoiceModel input);
    }
}