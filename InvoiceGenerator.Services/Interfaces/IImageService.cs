﻿using InvoiceGenerator.Common.Models.Image;
using InvoiceGenerator.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IImageService : IBaseService
    {
        public Task<ImageModel> GetAsync(long imageId);
        public Task<Image> AddAsync(IFormFile image);
    }
}