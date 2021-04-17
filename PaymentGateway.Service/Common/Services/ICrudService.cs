using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Common.Services
{
    public interface ICrudService<T>
        where T : Entity
    {
        Task<IEnumerable<T>> Get();

        Task<T> GetById(Guid guid);

        Task<int> Create(T entity);

        Task<int> Update(T entity);

        Task Delete(T entity);
    }
}
