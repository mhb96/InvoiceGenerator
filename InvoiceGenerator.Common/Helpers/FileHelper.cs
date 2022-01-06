using InvoiceGenerator.Common.DataTypes;
using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Extensions;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Common.Models.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InvoiceGenerator.Common.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ImageModel> UploadAsync(IFormFile file, FileType type)
        {
            var fileType = file.IsImage() ? FileType.image : FileType.invalid;
            if (fileType != type)
                throw new IGException("Invalid file type.");

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(wwwRootPath + $"\\upload\\{fileType}\\");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string extension = Path.GetExtension(file.FileName);
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("yymmssfff") + extension;
            string filePath = Path.Combine(uploadPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return new ImageModel { ImageFile = file, ImageName = fileName };
        }
        public string GetImageAddress(string fileName)
        {
            var uploadPath = "/upload/image/";
            string filePath = fileName == null ? Path.Combine(uploadPath, "placeholder.png") : Path.Combine(uploadPath, fileName);
            return filePath;
        }
    }
}
