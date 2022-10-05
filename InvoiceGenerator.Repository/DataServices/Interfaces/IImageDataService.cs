using InvoiceGenerator.Common.Models.Image;
using InvoiceGenerator.Entities;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices.Interfaces
{
    /// <summary>
    /// The currency data service interface.
    /// </summary>
    public interface IImageDataService
    {
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>
        /// The ImageModel.
        /// </returns>
        public Task<ImageModel> GetAsync(long id);

        /// <summary>
        /// Adds a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Task AddAsync(Image image);
    }
}
