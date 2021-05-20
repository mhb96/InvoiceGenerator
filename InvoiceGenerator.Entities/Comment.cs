namespace InvoiceGenerator.Entities
{
    public class Comment
    {
        /// <summary>
        /// Gets or sets the comment value.
        /// </summary>
        /// <value>
        /// The comment value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the invoice no.
        /// </summary>
        /// <value>
        /// The invoice no.
        /// </value>
        public long InvoiceNo { get; set; }

        /// <summary>
        /// Gets or sets the invoice.
        /// </summary>
        /// <value>
        /// The invoice.
        /// </value>
        public Invoice Invoice { get; set; }
    }
}
