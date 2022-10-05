using InvoiceGenerator.Common.Models.Invoice;
using InvoiceGenerator.Services.Models.Invoice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IInvoiceService : IBaseService
    {
        public Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId);
        public Task<InvoiceModel> GetAsync(long invoiceId, bool isForPdf);
        public Task DeleteAsync(long invoiceId, long userId);
        public Task<long> CreateAsync(CreateInvoiceModel input);
        public Task<string> CreateInvoicePdf(long invoiceId);
        public Task EditAsync(EditInvoiceModel input);
    }
}