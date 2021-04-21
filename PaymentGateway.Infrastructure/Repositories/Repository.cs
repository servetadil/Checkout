using Microsoft.EntityFrameworkCore;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Infrastructure.DatabaseFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Checkout.PaymentGateway.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly CheckoutWebDbContext _dbContext;
        private DbSet<T> _dbSet;

        public Repository(CheckoutWebDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            await Task.FromResult(_dbSet.Remove(entity));
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await GetAsync(id);
            await DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> wheres)
        {
            return await _dbSet.Where(wheres).ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }
        
        public async Task<T> SingleAsync(Expression<Func<T, bool>> wheres)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(wheres);

            return entity;
        }

        public async Task SaveChangesAsync()
        {
            if (await _dbContext.SaveChangesAsync() < 0)
            {
                throw new Exception("Cannot save changes in db.");
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            entity.LastUpdatedDateTime = DateTime.Now;
            await Task.FromResult(_dbSet.Update(entity));
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}