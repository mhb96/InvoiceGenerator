using InvoiceGenerator.Repository;
using Microsoft.Extensions.Logging;
using System;

namespace InvoiceGenerator.Services
{
    public class BaseService
    {
        private bool _disposed;

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        protected virtual IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected virtual ILogger<BaseService> Logger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IBaseService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public BaseService(IUnitOfWork unitOfWork, ILogger<BaseService> logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _disposed = true;
                UnitOfWork.Dispose();
            }
        }
    }
}
