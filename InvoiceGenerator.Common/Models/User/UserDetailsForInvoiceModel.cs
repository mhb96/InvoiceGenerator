namespace InvoiceGenerator.Common.Models.User
{
    /// <summary>
    /// The user details for invoice model.
    /// </summary>
    public class UserDetailsForInvoiceModel
    {
        /// <summary>
        /// The company name.
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// The address.
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// The contact no.
        /// </summary>
        public string ContactNo { get; set; }
        
        /// <summary>
        /// The email.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// The logo.
        /// </summary>
        public string Logo { get; set; }
        
        /// <summary>
        /// The decimal.
        /// </summary>
        public decimal Vat { get; set; }
        
        /// <summary>
        /// The currency identifier.
        /// </summary>
        public long CurrencyId { get; set; }
    }
}
