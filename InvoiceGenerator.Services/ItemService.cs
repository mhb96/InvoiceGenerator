using InvoiceGenerator.Common.Models;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class ItemService : BaseService, IItemService
    {
        public ItemService(IUnitOfWork unitOfWork, ILogger<InvoiceService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task AddAsync(List<ItemModel> items, long invoiceNo)
        {
            Logger.LogInformation($"Adding new items: {items} for invoice : {invoiceNo}.");

            var newItems = new List<Item>();
            foreach (var item in items)
            {
                Item newItem = new()
                {
                    CreatedAt = DateTime.Now,
                    InvoiceNo = invoiceNo,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    IsDeleted = false
                };
                newItems.Add(newItem);
            }
            await UnitOfWork.AddRangeAsync(newItems);
            await UnitOfWork.SaveAsync();
            Logger.LogInformation($"Added items for invoice : {invoiceNo}.");
        }
    }
}
