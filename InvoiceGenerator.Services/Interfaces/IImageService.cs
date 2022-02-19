using InvoiceGenerator.Common.Models.Image;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IImageService : IBaseService
    {
        public Task<ImageModel> GetAsync(long imageId);
    }
}