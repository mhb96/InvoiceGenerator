using InvoiceGenerator.Services.Models;
using System.Collections.Generic;

namespace InvoiceGenerator.Models
{
    public class DashoardOutputViewModel
    {
        public List<InvoiceSummaryModel> Invoices { get; set; }
    }
}