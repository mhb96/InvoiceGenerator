using InvoiceGenerator.Common.Models.Item;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IItemService : IBaseService
    {
        public Task<List<ItemOutputModel>> GetAsync(long invoiceNo);
        public Task AddAsync(List<ItemInputModel> items, long invoiceNo);
        public Task DeleteAllAsync(long invoiceNo);
    }
}