using InvoiceGenerator.Common.Models.Item;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices.Interfaces
{
    /// <summary>
    /// The item data service interface.
    /// </summary>
    public interface IItemDataService
    {
        /// <summary>
        /// Gets items for the invoice asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List of ItemInputModel.
        /// </returns>
        public Task<List<ItemOutputModel>> GetAsync(long invoiceNo);

        /// <summary>
        /// Adds list of items to the invoice.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public Task AddAsync(List<ItemInputModel> items, long invoiceNo);

        /// <summary>
        /// Deletes all items of an invoice.
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public Task DeleteAllAsync(long invoiceNo);
    }
}
