using InvoiceGenerator.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Defines the image entity.
    /// </summary>
    public class Image : BaseEntity
    {
        /// <summary>
        /// Gets or sets the image name.
        /// </summary>
        /// <value>
        /// The image name.
        /// </value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the image file.
        /// </summary>
        /// <value>
        /// The image file.
        /// </value>
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
