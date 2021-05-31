using InvoiceGenerator.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository
{
    /// <summary>
    /// Definition of Unit of Work.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        Task AddAsync<T>(T entity) where T : class, IBaseEntity;

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Update<T>(T entity) where T : class, IBaseEntity;

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Delete<T>(T entity) where T : class, IBaseEntity;

        /// <summary>
        /// Delete the entity in a soft manner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void SoftDelete<T>(T entity) where T : class, IBaseEntity;

        /// <summary>
        /// Gets the first instance matching <paramref name="expression"/> or default asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity;

        /// <summary>
        /// Queries the specified expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity;

        /// <summary>
        /// Queries the specified expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> Query<T>() where T : class, IBaseEntity;

        /// <summary>
        /// Check if any entity exist asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity;

        /// <summary>
        /// Saves the asynchronously.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// Executes the in transaction asynchronously.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        Task ExecuteInTransactionAsync(Func<IUnitOfWork, Task> action);

        /// <summary>
        /// Executes the in transaction asynchronously.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns></returns>
        Task ExecuteInTransactionAsync(Func<IUnitOfWork, Task> action, IsolationLevel isolationLevel);

        /// <summary>
        /// Adds the range asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity;

        /// <summary>
        /// Deletes the range asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        void DeleteRange<T>(IEnumerable<T> entities) where T : class, IBaseEntity;

        /// <summary>
        /// Updates the range asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        void UpdateRange<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
    }
}
