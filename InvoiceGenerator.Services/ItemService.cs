using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Extensions;
using InvoiceGenerator.Common.Models.Item;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class ItemService : BaseService, IItemService
    {
        private IItemDataService _itemDataService;
        public ItemService(ILogger<InvoiceService> logger, IItemDataService itemDataService) : base(logger)
        {
            _itemDataService = itemDataService;
        }

        public async Task<List<ItemOutputModel>> GetAsync(long invoiceNo) =>
            await _itemDataService.GetAsync(invoiceNo);

        public async Task AddAsync(List<ItemInputModel> items, long invoiceNo)
        {
            foreach (var item in items)
            {
                item.Name.ValidateString(item.Name, RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            }
            await _itemDataService.AddAsync(items, invoiceNo);
        }

        public async Task DeleteAllAsync(long invoiceNo) =>
            await _itemDataService.DeleteAllAsync(invoiceNo);
    }
}
