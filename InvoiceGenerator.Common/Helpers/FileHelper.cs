using InvoiceGenerator.Common.DataTypes;
using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Extensions;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Common.Models.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SelectPdf;
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

        public string GetImageAddress(string fileName, bool isForPdf = false)
        {
            string uploadPath;
            if (isForPdf)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                uploadPath = Path.Combine(wwwRootPath + $"\\upload\\image\\");
            }
            else uploadPath = "/upload/image/";
            string filePath = Path.Combine(uploadPath, fileName);
            return filePath;
        }

        public string CreatePdf(string html)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(html);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(wwwRootPath + $"\\temp");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            var fileName = $"{DateTime.Now:yymmssffffff}";
            var filePath = $"{uploadPath}\\{fileName}.pdf";
            doc.Save(filePath);
            return fileName;
        }

        public void DeleteAllTempFiles()
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var tempPath = Path.Combine(wwwRootPath + $"\\temp");
            if (!Directory.Exists(tempPath))
                return;
            DirectoryInfo di = new DirectoryInfo(tempPath);
            foreach (FileInfo file in di.EnumerateFiles())
                file.Delete();
            return;
        }
    }
}
