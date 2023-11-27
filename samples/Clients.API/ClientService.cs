using NetSwissTools.Exceptions;
using NetSwissTools.Models;
using NetSwissTools.System;
using NetSwissTools.Web.Mvc.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace Clients.API
{
    public class ClientService :
        IReaderService<Client>,
        IWriterService<Client>
    {
        public List<ModelException> Errors { get; set; }

        public ClientService()
        {
            Errors = new List<ModelException>();
        }

        public async Task<Client> FirstOrDefaultAsync(Expression<Func<Client, bool>> expr)
        {
            await Task.Delay(100);
            lock (MemoryStore.Clients)
            {
                return MemoryStore.Clients.FirstOrDefault(expr.Compile());
            }
        }

        public Task<Client> FirstOrDefaultAsync(Expression<Func<Client, bool>> expr, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FirstOrDefaultAsync(Expression<Func<Client, bool>> expr, Expression<Func<Client, object>>[] includeProperties, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<Client>> GetAllPagedAsync(int page, int pageSize)
        {
            await Task.Delay(100);
            lock (MemoryStore.Clients)
            {
                var pageList = MemoryStore.Clients
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return new PagedResult<Client>
                {
                    CurrentPage = page,
                    PageCount = pageList.Count(),
                    HasNext = false,
                    PageSize = pageSize,
                    Results = pageList.ToList(),
                    RowCount = pageList.Count(),
                };
            }
        }

        public async Task<PagedResult<Client>> GetAllPagedAsync(int page, int pageSize, CancellationToken cancellation)
        {
            await Task.Delay(100);
            lock (MemoryStore.Clients)
            {
                var pageList = MemoryStore.Clients
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return new PagedResult<Client>
                {
                    CurrentPage = page,
                    PageCount = pageList.Count(),
                    HasNext = false,
                    PageSize = pageSize,
                    Results = pageList.ToList(),
                    RowCount = pageList.Count(),
                };
            }
        }

        public async Task<PagedResult<Client>> GetAllPagedAsync(int page, int pageSize, Expression<Func<Client, object>>[] includeProperties, CancellationToken cancellation)
        {
            await Task.Delay(100);
            lock (MemoryStore.Clients)
            {
                var pageList = MemoryStore.Clients
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return new PagedResult<Client>
                {
                    CurrentPage = page,
                    PageCount = pageList.Count(),
                    HasNext = false,
                    PageSize = pageSize,
                    Results = pageList.ToList(),
                    RowCount = pageList.Count(),
                };
            }
        }

        public Task<PagedResult<Client>> GetAllPagedFilteredAsync(int page, int pageSize, Expression<Func<Client, bool>> expr)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Client>> GetAllPagedFilteredAsync(int page, int pageSize, Expression<Func<Client, bool>> expr, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Client>> GetAllPagedFilteredAsync(int page, int pageSize, Expression<Func<Client, bool>> expr, Expression<Func<Client, object>>[] includeProperties, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetByKeyAsync(Expression<Func<Client, bool>> expr)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetByKeyAsync(Expression<Func<Client, bool>> expr, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetByKeyAsync(Expression<Func<Client, bool>> expr, Expression<Func<Client, object>>[] includeProperties, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> QueryAsync(Expression<Func<Client, bool>> expr)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> QueryAsync(Expression<Func<Client, bool>> expr, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> QueryAsync(Expression<Func<Client, bool>> expr, Expression<Func<Client, object>>[] includeProperties, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Client Add(Client model)
        {
            lock (MemoryStore.Clients)
            {
                model.Id = Guid.NewGuid();

                MemoryStore.Clients.Add(model);
            }

            return model;
        }

        public Client Update(Client model)
        {
            lock (MemoryStore.Clients)
            {
                var client = MemoryStore.Clients.Find(x => x.Id == model.Id);

                client.CopyProperties(model);
            }

            return model;
        }

        public Client Remove(Client model)
        {
            throw new NotImplementedException();
        }

        public Task<Client> RemoveByKeyAsync(Expression<Func<Client, bool>> expr)
        {
            throw new NotImplementedException();
        }

        public Task<Client> RemoveByKeyAsync(Expression<Func<Client, bool>> expr, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
