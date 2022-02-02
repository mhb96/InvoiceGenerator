namespace InvoiceGenerator.Services.Models
{
    public class InvoiceSummaryModel
    {
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public string InvoiceNo { get; set; }
        public string ToCompany { get; set; }
        public string TotalFee { get; set; }
    }
}
