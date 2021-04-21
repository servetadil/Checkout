using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Common.Services
{
    public class CrudService<T> : ICrudService<T>
           where T : Entity
    {
        protected readonly IRepository<T> _repository;

        public CrudService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(T entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            entity.LastUpdatedDateTime = DateTime.Now;

            await _repository.CreateAsync(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> Update(T entity)
        {
            entity.LastUpdatedDateTime = DateTime.Now;
            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> wheres)
        {
            return await _repository.GetAllAsync(wheres);
        }

        public virtual async Task<T> GetById(Guid Id)
        {
            return await _repository.GetAsync(Id);
        }

        public virtual async Task Delete(T entity)
        {
            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();
        }
    }
}