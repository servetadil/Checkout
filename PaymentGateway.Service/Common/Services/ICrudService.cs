using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Common.Services
{
    public interface ICrudService<T>
        where T : Entity
    {
        Task<int> Create(T entity);

        Task<int> Update(T entity);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> wheres);

        Task<T> GetById(Guid guid);

        Task Delete(T entity);
    }
}
