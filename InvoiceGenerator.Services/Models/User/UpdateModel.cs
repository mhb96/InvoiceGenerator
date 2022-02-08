using Microsoft.AspNetCore.Http;

namespace InvoiceGenerator.Services.Models.User
{
    public class UpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public decimal Vat { get; set; }
        public IFormFile CompanyLogo { get; set; }
        public long CurrencyId { get; set; }
    }
}
