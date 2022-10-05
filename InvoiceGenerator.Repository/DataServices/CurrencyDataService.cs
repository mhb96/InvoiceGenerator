using InvoiceGenerator.Common.Models.Currency;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices
{
    /// <summary>
    /// The currency data service class.
    /// </summary>
    public class CurrencyDataService : BaseDataService, ICurrencyDataService
    {
        /// <summary>
        /// The currency data service constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public CurrencyDataService(IUnitOfWork unitOfWork, ILogger<CurrencyDataService> logger) : base(unitOfWork, logger)
        { }

        ///<inheritdoc/>
        public async Task<List<CurrencyModel>> GetAsync()
        {
            var currencies = await UnitOfWork.Query<Currency>().Select(c => new CurrencyModel { Id = c.Id, Code = c.Code, Name = $"{c.Code} - {c.Name}" }).ToListAsync();
            return currencies.OrderBy(c => c.Code).ToList();
        }
    }
}
