namespace InvoiceGenerator.Common.Models.Item
{
    /// <summary>
    /// The item output model.
    /// </summary>
    public class ItemOutputModel
    {
        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The quantity.
        /// </summary>
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// The unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// The total price.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
