using System;

namespace InvoiceGenerator.Entities.Interfaces
{
    /// <summary>
    /// Defines the base entity.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        DateTime CreatedAt { get; set; }
    }
}