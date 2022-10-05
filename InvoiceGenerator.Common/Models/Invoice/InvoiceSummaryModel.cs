using InvoiceGenerator.Common.DataTypes;

namespace InvoiceGenerator.Common.Models.Invoice
{
    /// <summary>
    /// The invoice summary model.
    /// </summary>
    public class InvoiceSummaryModel
    {
        /// <summary>
        /// The created date.
        /// </summary>
        public string CreatedDate { get; set; }
        
        /// <summary>
        /// The due date.
        /// </summary>
        public string DueDate { get; set; }

        /// <summary>
        /// The invoice no.
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// The to company name.
        /// </summary>
        public string ToCompany { get; set; }

        /// <summary>
        /// The total fee.
        /// </summary>
        public string TotalFee { get; set; }

        /// <summary>
        /// The payment status.
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }
    }
}
