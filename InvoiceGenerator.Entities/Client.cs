namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Implements the client entity.
    /// </summary>
    /// <seealso cref="InvoiceGenerator.Entities.BaseEntity" />
    public class Client : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

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