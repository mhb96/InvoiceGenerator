using InvoiceGenerator.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Implements the role entity
    /// </summary>
    public class Role : IdentityRole<long>, IRole
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }
    }
}