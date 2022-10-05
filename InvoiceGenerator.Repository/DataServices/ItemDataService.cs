using InvoiceGenerator.Common.Models.Item;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices
{
    /// <summary>
    /// The item data service class
    /// </summary>
    public class ItemDataService : BaseDataService, IItemDataService
    {
        public ItemDataService(IUnitOfWork unitOfWork, ILogger<UserDataService> logger) : base(unitOfWork, logger)
        {
        }

        ///<inheritdoc/>
        public async Task<List<ItemOutputModel>> GetAsync(long invoiceNo)
        {
            Logger.LogInformation($"Getting items for invoice : {invoiceNo}.");

            return await UnitOfWork.Query<Item>(i => i.InvoiceNo == invoiceNo).Select(i => new ItemOutputModel
            {
                Name = i.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = Math.Round(i.Quantity * i.UnitPrice, 2, MidpointRounding.AwayFromZero)
            }).ToListAsync();
        }

        ///<inheritdoc/>
        public async Task AddAsync(List<ItemInputModel> items, long invoiceNo)
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

        ///<inheritdoc/>
        public async Task DeleteAllAsync(long invoiceNo)
        {
            Logger.LogInformation($"Deleting all items for invoice : {invoiceNo}.");
            var items = await UnitOfWork.Query<Item>(i => i.InvoiceNo == invoiceNo).ToListAsync();
            UnitOfWork.DeleteRange(items);
            await UnitOfWork.SaveAsync();
        }
    }
}
