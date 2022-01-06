using InvoiceGenerator.Common.Models;
using System.Collections.Generic;

namespace InvoiceGenerator.Services.Models.Invoice
{
    public class CreateInvoiceModel
    {
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public List<ItemModel> Items { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalFee { get; set; }
        public string Comment { get; set; }
    }
}
