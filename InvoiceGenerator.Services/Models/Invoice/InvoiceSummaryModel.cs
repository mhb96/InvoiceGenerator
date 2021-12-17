using Microsoft.AspNetCore.Http;
using System;

namespace InvoiceGenerator.Services.Models
{
    public class InvoiceSummaryModel
    {
        public DateTime Date { get; set; }
        public long InvoiceNo { get; set; }
        public string ToCompany { get; set; }
        public decimal TotalFee { get; set; }
    }
}
