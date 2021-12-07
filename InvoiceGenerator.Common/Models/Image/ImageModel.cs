using Microsoft.AspNetCore.Http;

namespace InvoiceGenerator.Common.Models.Image
{
    public class ImageModel
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
        public IFormFile ImageFile { get; set; }
    }
}
