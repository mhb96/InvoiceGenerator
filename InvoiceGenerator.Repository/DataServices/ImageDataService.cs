using InvoiceGenerator.Common.Models.Image;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices
{
    /// <summary>
    /// The image data service.
    /// </summary>
    public class ImageDataService : BaseDataService, IImageDataService
    {
        public ImageDataService(IUnitOfWork unitOfWork, ILogger<ImageDataService> logger) : base(unitOfWork, logger)
        { }

        ///<inheritdoc/>
        public async Task<ImageModel> GetAsync(long id) =>
            await UnitOfWork.Query<Image>(i => i.Id == id).Select(i => new ImageModel { ImageName = i.ImageName }).FirstOrDefaultAsync();

        ///<inheritdoc/>
        public async Task AddAsync(Image image) =>
            await UnitOfWork.AddAsync(image);
    }
}
