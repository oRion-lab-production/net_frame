using NHibernate.Criterion;
using NHibernate;
using Tools.DataAccess;

namespace Structures.Conn_nHibernate
{
    public class RepoBase<T> : IRepository<T> where T : class
    {
        private IAppSessionBuilder _sessionBuilder { get; }
        private ISession _session { get; set; }

        public RepoBase(IAppSessionBuilder sessionBuilder)
        {
            _sessionBuilder = sessionBuilder;
            _session = _sessionBuilder.GetSession();
        }

        public void StopExecutedQuery() => _session.CancelQuery();

        public bool IsContain(T entity) => _session.Contains(entity);
        public void Evict(T entity) => _session.Evict(entity);
        public void Refresh(T entity) => _session.Refresh(entity);
        public void RefreshLock(T entity) => _session.Refresh(entity, LockMode.Upgrade);

        public async Task EvictAsync(T entity) => await _session.EvictAsync(entity);
        public async Task RefreshAsync(T entity) => await _session.RefreshAsync(entity);
        public async Task RefreshLockAsync(T entity) => await _session.RefreshAsync(entity, LockMode.Upgrade);

        public T Get(object id) => _session.Get<T>(id);
        public T GetLock(object id) => _session.Get<T>(id, LockMode.Upgrade);

        public async Task<T> GetAsync(object id) => await _session.GetAsync<T>(id);
        public async Task<T> GetAsync(object id, CancellationToken cancellationToken) => await _session.GetAsync<T>(id, cancellationToken);
        public async Task<T> GetLockAsync(object id) => await _session.GetAsync<T>(id, LockMode.Upgrade);
        public async Task<T> GetLockAsync(object id, CancellationToken cancellationToken) => await _session.GetAsync<T>(id, LockMode.Upgrade, cancellationToken);

        public IQueryable<T> Query() => _session.Query<T>();

        public ICriteria Criteria() => _session.CreateCriteria(typeof(T));
        public ICriteria CriteriaLock() => _session.CreateCriteria(typeof(T)).SetLockMode(LockMode.Upgrade);

        public ICriteria AddCriterion(ICriterion criterion) => Criteria().Add(criterion);
        public ICriteria AddCriterionLock(ICriterion criterion) => CriteriaLock().Add(criterion);

        public ICriteria CountCriteria() => Criteria().SetProjection(Projections.RowCount());
        public int Count() => Criteria().SetProjection(Projections.RowCount()).UniqueResult<int>();

        public async Task<int> CountAsync() => await Criteria().SetProjection(Projections.RowCount()).UniqueResultAsync<int>();

        public IList<T> List() => Criteria().List<T>();
        public IList<T> ListLock() => CriteriaLock().List<T>();

        public async Task<IList<T>> ListAsync() => await Criteria().ListAsync<T>();
        public async Task<IList<T>> ListAsync(CancellationToken cancellationToken) => await Criteria().ListAsync<T>(cancellationToken);
        public async Task<IList<T>> ListLockAsync() => await CriteriaLock().ListAsync<T>();
        public async Task<IList<T>> ListLockAsync(CancellationToken cancellationToken) => await CriteriaLock().ListAsync<T>(cancellationToken);

        public void Save(T entity) => _session.Save(entity);
        public void Update(T entity) => _session.Update(entity);
        public void Delete(T entity) => _session.Delete(entity);
        public void Purge() => _session.CreateQuery($"DELETE FROM {typeof(T).FullName}").ExecuteUpdate();

        public void Delete(string propertyName, object propertyValue) => _session
            .CreateQuery($"DELETE FROM {typeof(T).FullName} o WHERE o.{propertyName} = (:propertyValue)")
            .SetParameter("propertyValue", propertyValue)
            .ExecuteUpdate();

        public async Task SaveAsync(T entity) => await _session.SaveAsync(entity);
        public async Task SaveAsync(T entity, CancellationToken cancellationToken) => await _session.SaveAsync(entity, cancellationToken);
        public async Task UpdateAsync(T entity) => await _session.UpdateAsync(entity);
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken) => await _session.UpdateAsync(entity, cancellationToken);
        public async Task DeleteAsync(T entity) => await _session.DeleteAsync(entity);
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken) => await _session.DeleteAsync(entity, cancellationToken);
        public async Task PurgeAsync() => await _session.CreateQuery($"DELETE FROM {typeof(T).FullName}").ExecuteUpdateAsync();

        public async Task DeleteAsync(string propertyName, object propertyValue) => await _session
            .CreateQuery($"DELETE FROM {typeof(T).FullName} o WHERE o.{propertyName} = (:propertyValue)")
            .SetParameter("propertyValue", propertyValue)
            .ExecuteUpdateAsync();
    }
}
