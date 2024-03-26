using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tools.DataAccess
{
    public interface IEFRepository<T> where T : class
    {
        T Get(object id);
        Task<T> GetAsync(object id);

        IQueryable<T> List(Expression<Func<T, bool>> expression);

        void Add(T entity);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        Task UpdateAsync(Expression<Func<T, T>> expression, CancellationToken cancellationToken = default);
        void Delete(T entity);
    }
}
