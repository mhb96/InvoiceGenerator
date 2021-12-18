using InvoiceGenerator.Services.Models;
using System.Collections.Generic;

namespace InvoiceGenerator.Models
{
    public class DashboardOutputViewModel
    {
        public List<InvoiceSummaryModel> Invoices { get; set; }
    }
}