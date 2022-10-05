using InvoiceGenerator.Common.DataTypes;
using InvoiceGenerator.Common.Models.Image;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InvoiceGenerator.Common.Helpers.Interfaces
{
    /// <summary>
    /// The file helper interface.
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Upload file asynchronously.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <returns>
        /// Image model.
        /// </returns>
        public Task<ImageModel> UploadAsync(IFormFile file, FileType type);
        
        /// <summary>
        /// Get the image address.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isForPdf"></param>
        /// <returns>
        /// The address.
        /// </returns>
        public string GetImageAddress(string fileName, bool isForPdf = false);
        
        /// <summary>
        /// Creates pdf.
        /// </summary>
        /// <param name="html"></param>
        /// <returns>
        /// The file name
        /// </returns>
        public string CreatePdf(string html);

        /// <summary>
        /// Deletes All temp files.
        /// </summary>
        public void DeleteAllTempFiles();
    }
}
