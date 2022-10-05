using InvoiceGenerator.Common.Models.Currency;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class CurrencyService : BaseService, ICurrencyService
    {
        private readonly ICurrencyDataService _currencyDataService;
        public CurrencyService(ILogger<BaseService> logger, ICurrencyDataService currencyDataService) : base(logger)
        {
            _currencyDataService = currencyDataService;
        }

        public async Task<List<CurrencyModel>> GetAsync() => await _currencyDataService.GetAsync();
    }
}