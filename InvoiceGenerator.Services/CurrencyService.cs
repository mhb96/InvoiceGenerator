using InvoiceGenerator.Common.Models.Currency;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class CurrencyService : BaseService, ICurrencyService
    {
        public CurrencyService(IUnitOfWork unitOfWork, ILogger<BaseService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<List<CurrencyModel>> GetAsync()
        {
            var currencies = await UnitOfWork.Query<Currency>().Select(c => new CurrencyModel {Id = c.Id, Code = c.Code, Name = $"{c.Code} - {c.Name}" }).ToListAsync();
            return currencies.OrderBy(c => c.Code).ToList();
        }
    }
}