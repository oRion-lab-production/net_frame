using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tools.DataAccess;

namespace Structures.Conn_EFCore
{
    public class EFUnitOfWork<AppDbContext> : IEFUnitOfWork, IDisposable where AppDbContext : class
    {
        private EFFactoryBuilder<AppDbContext> _dbFactory;

        public EFUnitOfWork(EFFactoryBuilder<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        private static string GetIsolationLevel(IsolationLevel isolationLvl)
        {
            return isolationLvl switch {
                IsolationLevel.ReadUncommitted => "READ UNCOMMITTED",
                IsolationLevel.RepeatableRead => "REPEATABLE READ",
                IsolationLevel.Serializable => "SERIALIZABLE",
                _ => "READ COMMITTED"
            };
        }

        public bool OpenConnection() => _dbFactory.DbContext.Database.CanConnect();
        public async Task<bool> OpenConnectionAsync(CancellationToken cancellationToken = default) => await _dbFactory.DbContext.Database.CanConnectAsync(cancellationToken);

        public bool EnsureCreated() => _dbFactory.DbContext.Database.EnsureCreated();
        public async Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default) => await _dbFactory.DbContext.Database.EnsureCreatedAsync(cancellationToken);
        
        public bool EnsureDeleted() => _dbFactory.DbContext.Database.EnsureDeleted();
        public async Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = default) => await _dbFactory.DbContext.Database.EnsureDeletedAsync(cancellationToken);
        
        public void SetIsolationLevel(IsolationLevel isolationLvl = IsolationLevel.ReadCommitted) => _dbFactory.DbContext.Database.ExecuteSqlRaw($"SET TRANSACTION ISOLATION LEVEL {GetIsolationLevel(isolationLvl)};");
        public async Task SetIsolationLevelAsync(IsolationLevel isolationLvl = IsolationLevel.ReadCommitted) => await _dbFactory.DbContext.Database.ExecuteSqlRawAsync($"SET TRANSACTION ISOLATION LEVEL {GetIsolationLevel(isolationLvl)};");

        public void EnsureDBCleanState()
        {
            if (!OpenConnection()) {
                throw new InvalidOperationException("Connection was closed, Please ensure it's open!.");
            }

            if(!EnsureDeleted() && !EnsureCreated()) {
                throw new InvalidOperationException("Database not in clean state!.");
            }
        }

        public async Task EnsureDBCleanStateAsync()
        {
            if (!await OpenConnectionAsync()) {
                throw new InvalidOperationException("Connection was closed, Please ensure it's open!.");
            }

            if (!await EnsureDeletedAsync() && !await EnsureCreatedAsync()) {
                throw new InvalidOperationException("Database not in clean state!.");
            }
        }

        public void EvictChanges() => _dbFactory.DbContext.ChangeTracker.Clear();
        public bool HasCommittedChanges() => _dbFactory.DbContext.ChangeTracker.HasChanges();

        public void SaveChanges() => _dbFactory.DbContext.SaveChanges();
        public async Task SaveChangesAsync() => await _dbFactory.DbContext.SaveChangesAsync();

        private IDbContextTransaction GetTransaction() => _dbFactory.DbContext.Database.CurrentTransaction;

        public void BeginTransaction()
        {
            var transaction = GetTransaction();

            if (transaction != null) {
                transaction.Dispose();
            }

            _dbFactory.DbContext.Database.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            var transaction = GetTransaction();

            if (transaction != null) {
                transaction.Dispose();
            }

            SetIsolationLevel(isolationLevel);

            _dbFactory.DbContext.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            var transaction = GetTransaction();

            if (transaction != null) {
                transaction.Dispose();
            }

            await _dbFactory.DbContext.Database.BeginTransactionAsync();
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            var transaction = GetTransaction();

            if (transaction != null) {
                transaction.Dispose();
            }

            await SetIsolationLevelAsync(isolationLevel);

            await _dbFactory.DbContext.Database.BeginTransactionAsync();
        }

        public void Commit() => GetTransaction().Commit();

        public async Task CommitAsync() => await GetTransaction().CommitAsync();

        public void Rollback() => GetTransaction().Rollback();

        public async Task RollbackAsync() => await GetTransaction().RollbackAsync();

        public void Dispose()
        {
            var transaction = GetTransaction();

            if (transaction != null) 
                transaction.Dispose();
        }
    }
}
