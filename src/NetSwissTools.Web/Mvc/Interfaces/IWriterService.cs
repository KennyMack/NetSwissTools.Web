using NetSwissTools.Services.Interfaces;
using System.Linq.Expressions;

namespace NetSwissTools.Web.Mvc.Interfaces
{
    public interface IWriterService<T> : IErrorBaseService where T : class
    {
        T Add(T model);
        T Update(T model);
        T Remove(T model);

        Task<T> RemoveByKeyAsync(Expression<Func<T, bool>> expr);
        Task<T> RemoveByKeyAsync(Expression<Func<T, bool>> expr, CancellationToken cancellation);

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellation);
    }
}
