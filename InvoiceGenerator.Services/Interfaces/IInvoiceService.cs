using InvoiceGenerator.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IInvoiceService : IBaseService
    {
        public Task<List<InvoiceSummaryModel>> GetForDashboardAsync();
        public Task DeleteAsync(long id);
    }
}