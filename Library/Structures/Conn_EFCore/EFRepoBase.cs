using FluentNHibernate.Data;
using Microsoft.EntityFrameworkCore;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools.DataAccess;

namespace Structures.Conn_EFCore
{
    public class EFRepoBase<T, AppDbContext> : IEFRepository<T> where T : class where AppDbContext : class
    {
        private readonly EFFactoryBuilder<AppDbContext> _dbFactory;

        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet { get => _dbSet ??= _dbFactory.DbContext.Set<T>(); }

        public EFRepoBase(EFFactoryBuilder<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public T Get(object id) => DbSet.Find(id);
        public async Task<T> GetAsync(object id) => await DbSet.FindAsync(id);

        public IQueryable<T> List(Expression<Func<T, bool>> expression) => DbSet.Where(expression);

        public void Add(T entity) => DbSet.Add(entity);
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default) => await DbSet.AddAsync(entity, cancellationToken);
        public void Update(T entity) => DbSet.Update(entity);
        public async Task UpdateAsync(Expression<Func<T, T>> expression, CancellationToken cancellationToken = default) => await DbSet.UpdateAsync(expression, cancellationToken);
        public void Delete(T entity) => DbSet.Remove(entity);
    }
}
