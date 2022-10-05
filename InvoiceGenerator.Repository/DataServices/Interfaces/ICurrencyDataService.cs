using InvoiceGenerator.Common.Models.Currency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices.Interfaces
{
    /// <summary>
    /// The currency data service interface.
    /// </summary>
    public interface ICurrencyDataService
    {
        /// <summary>
        /// Gets all currencies.
        /// </summary>
        /// <returns></returns>
        public Task<List<CurrencyModel>> GetAsync();
    }
}
