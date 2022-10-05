namespace InvoiceGenerator.Common.Models.Currency
{
    /// <summary>
    /// The currency model.
    /// </summary>
    public class CurrencyModel
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The code.
        /// </summary>
        public string Code { get; set; }
    }
}
