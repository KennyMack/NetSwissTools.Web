using NetSwissTools.Models;
using NetSwissTools.Services.Interfaces;
using System.Linq.Expressions;

namespace NetSwissTools.Web.Mvc.Interfaces
{
    public interface IReaderService<T> : IErrorBaseService where T : class
    {
        Task<PagedResult<T>> GetAllPagedAsync(int page, int pageSize);
        Task<PagedResult<T>> GetAllPagedAsync(int page, int pageSize, CancellationToken cancellation);
        Task<PagedResult<T>> GetAllPagedAsync(int page, int pageSize, Expression<Func<T, object>>[] includeProperties, CancellationToken cancellation);

        Task<PagedResult<T>> GetAllPagedFilteredAsync(int page, int pageSize, Expression<Func<T, bool>> expr);
        Task<PagedResult<T>> GetAllPagedFilteredAsync(int page, int pageSize, Expression<Func<T, bool>> expr, CancellationToken cancellation);
        Task<PagedResult<T>> GetAllPagedFilteredAsync(int page, int pageSize, Expression<Func<T, bool>> expr, Expression<Func<T, object>>[] includeProperties, CancellationToken cancellation);

        Task<T> GetByKeyAsync(Expression<Func<T, bool>> expr);
        Task<T> GetByKeyAsync(Expression<Func<T, bool>> expr, CancellationToken cancellation);
        Task<T> GetByKeyAsync(Expression<Func<T, bool>> expr, Expression<Func<T, object>>[] includeProperties, CancellationToken cancellation);

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> expr);
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> expr, CancellationToken cancellation);
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> expr, Expression<Func<T, object>>[] includeProperties, CancellationToken cancellation);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expr);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expr, CancellationToken cancellation);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expr, Expression<Func<T, object>>[] includeProperties, CancellationToken cancellation);
    }
}
