namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Implements the commnet entity.
    /// </summary>
    /// <seealso cref="InvoiceGenerator.Entities.BaseEntity" />
    public class Comment : BaseEntity
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
        public virtual Invoice Invoice { get; set; }
    }
}