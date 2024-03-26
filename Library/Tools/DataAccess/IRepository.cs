using NHibernate;
using NHibernate.Criterion;

namespace Tools.DataAccess
{
    public interface IRepository<T> where T : class
    {
        void StopExecutedQuery();

        bool IsContain(T entity);
        void Evict(T entity);
        void Refresh(T entity);
        void RefreshLock(T entity);

        Task EvictAsync(T entity);
        Task RefreshAsync(T entity);
        Task RefreshLockAsync(T entity);

        T Get(object id);
        T GetLock(object id);

        Task<T> GetAsync(object id);
        Task<T> GetAsync(object id, CancellationToken cancellationToken);
        Task<T> GetLockAsync(object id);
        Task<T> GetLockAsync(object id, CancellationToken cancellationToken);

        IQueryable<T> Query();

        ICriteria Criteria();
        ICriteria CriteriaLock();

        ICriteria AddCriterion(ICriterion criterion);
        ICriteria AddCriterionLock(ICriterion criterion);

        ICriteria CountCriteria();
        int Count();

        Task<int> CountAsync();

        IList<T> List();
        IList<T> ListLock();

        Task<IList<T>> ListAsync();
        Task<IList<T>> ListAsync(CancellationToken cancellationToken);
        Task<IList<T>> ListLockAsync();
        Task<IList<T>> ListLockAsync(CancellationToken cancellationToken);

        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Purge();

        void Delete(string propertyName, object propertyValue);

        Task SaveAsync(T entity);
        Task SaveAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
        Task PurgeAsync();

        Task DeleteAsync(string propertyName, object propertyValue);
    }
}
