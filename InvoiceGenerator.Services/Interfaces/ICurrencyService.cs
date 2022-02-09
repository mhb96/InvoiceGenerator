using InvoiceGenerator.Common.Models.Currency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface ICurrencyService : IBaseService
    {
        public Task<List<CurrencyModel>> GetAsync();
    }
}