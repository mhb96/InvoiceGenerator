using InvoiceGenerator.Entities;

namespace InvoiceGenerator.Repository.Models.User
{
    public class UpdateDataModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public decimal Vat { get; set; }
        public long CurrencyId { get; set; }
        public Image CompanyLogo { get; set; }
    }
}
