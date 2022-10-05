using System;

namespace InvoiceGenerator.Repository.Models.Invoice
{
    public class CreateInvoiceDataModel
    {
        public long UserId { get; set; }
        public string UserAddress { get; set; }
        public string UserCompanyName { get; set; }
        public string UserContactNo { get; set; }
        public string UserEmail { get; set; }
        public long UserCompanyLogoId { get; set; }
        public string ClientAddress { get; set; }
        public string ClientName { get; set; }
        public string Comment { get; set; }
        public string ClientCompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ClientEmailAddress { get; set; }
        public string ClientPhoneNumber { get; set; }
        public decimal TotalFee { get; set; }
        public decimal Vat { get; set; }
        public long CurrencyId { get; set; }
        public decimal FeePaid { get; set; }
    }
}
