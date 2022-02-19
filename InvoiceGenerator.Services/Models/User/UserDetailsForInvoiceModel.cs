namespace InvoiceGenerator.Services.Models.User
{
    public class UserDetailsForInvoiceModel
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public decimal Vat { get; set; }
        public long CurrencyId { get; set; }
    }
}
