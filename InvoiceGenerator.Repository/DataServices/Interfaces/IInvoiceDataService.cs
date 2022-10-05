using InvoiceGenerator.Common.Models.Invoice;
using InvoiceGenerator.Repository.Models.Invoice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices.Interfaces
{
    /// <summary>
    /// The invoice data service interface.
    /// </summary>
    public interface IInvoiceDataService
    {
        /// <summary>
        /// Gets list of InvoiceSummaryModel for dashboard asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// A list of InvoiceSummaryModel
        /// </returns>
        public Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId);

        /// <summary>
        /// Gets invoice data asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Invoice data model
        /// </returns>
        public Task<InvoiceModel> GetAsync(long userId);

        /// <summary>
        /// Deletes invoice asynchronously.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userId"></param>
        public Task DeleteAsync(long invoiceId, long userId);

        /// <summary>
        /// Edits invoice asynchronously.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userId"></param>
        /// <param name="editInvoiceModel"></param>
        /// <returns></returns>
        public Task EditAsync(long invoiceId, long userId, EditInvoiceDataModel editInvoiceModel);

        /// <summary>
        /// Creates invoice asynchronously.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="userId"></param>
        /// <param name="editInvoiceModel"></param>
        /// <returns></returns>
        public Task<long> CreateAsync(CreateInvoiceDataModel editInvoiceModel);
    }
}
