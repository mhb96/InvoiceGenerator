using InvoiceGenerator.Common.DataTypes;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Common.Models.Image;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class ImageService : BaseService, IImageService
    {
        IImageDataService _imageDataService;
        IFileHelper _fileHelper;
        public ImageService(ILogger<BaseService> logger, IImageDataService imageDataService, IFileHelper fileHelper) : base(logger)
        {
            _imageDataService = imageDataService;
            _fileHelper = fileHelper;
        }

        public async Task<ImageModel> GetAsync(long id) =>
            await _imageDataService.GetAsync(id);

        public async Task<Image> AddAsync(IFormFile image)
        {
            ImageModel logo = await _fileHelper.UploadAsync(image, FileType.image);

            var newImage = new Image
            {
                CreatedAt = DateTime.Now,
                ImageFile = logo.ImageFile,
                ImageName = logo.ImageName
            };

            await _imageDataService.AddAsync(newImage);

            return newImage;
        }
    }
}
