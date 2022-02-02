using InvoiceGenerator.Common.DataTypes;
using InvoiceGenerator.Common.Models.Image;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InvoiceGenerator.Common.Helpers.Interfaces
{
    public interface IFileHelper
    {
        public Task<ImageModel> UploadAsync(IFormFile file, FileType type);
        public string GetImageAddress(string fileName, bool isForPdf = false);
        public string CreatePdf(string html);
        public void DeleteAllTempFiles();
    }
}
