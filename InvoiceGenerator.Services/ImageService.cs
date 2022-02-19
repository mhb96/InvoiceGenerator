using InvoiceGenerator.Common.Models.Image;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class ImageService : BaseService, IImageService
    {
        public ImageService(IUnitOfWork unitOfWork, ILogger<BaseService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<ImageModel> GetAsync(long id) =>
            await UnitOfWork.Query<Image>(i => i.Id == id).Select(i => new ImageModel { ImageName = i.ImageName }).FirstOrDefaultAsync();
    }
}
