using InvoiceGenerator.Common.Models.Item;
using System.Collections.Generic;

namespace InvoiceGenerator.Models
{
    public class CreateInvoiceInputModel
    {
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public List<ItemInputModel> Items { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalFee { get; set; }
        public string Comment { get; set; }
    }
}