using InvoiceGenerator.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IItemService : IBaseService
    {
        public Task AddAsync(List<ItemModel> items, long invoiceNo);
    }
}