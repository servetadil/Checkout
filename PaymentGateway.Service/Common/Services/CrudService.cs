using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
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
            var adding = entity.Id.Equals(default);

            await _repository.CreateAsync(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> Update(T entity)
        {
            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _repository.GetAllAsync();
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