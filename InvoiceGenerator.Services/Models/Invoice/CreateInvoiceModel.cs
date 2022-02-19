﻿using InvoiceGenerator.Common.Models.Item;
using System.Collections.Generic;

namespace InvoiceGenerator.Services.Models.Invoice
{
    public class EditInvoiceModel
    {
        public string UserCompanyName { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmailAddress { get; set; }
        public string ClientName { get; set; }
        public string ClientCompanyName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmailAddress { get; set; }
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public List<ItemInputModel> Items { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalFee { get; set; }
        public string Comment { get; set; }
        public long UserId { get; set; }
        public long CurrencyId { get; set; }
        public long InvoiceId { get; set; }
    }
}
