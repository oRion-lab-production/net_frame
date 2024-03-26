using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.DataAccess
{
    public interface IEFUnitOfWork : IDisposable
    {
        bool OpenConnection();
        Task<bool> OpenConnectionAsync(CancellationToken cancellationToken = default);

        bool EnsureCreated();
        Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default);

        bool EnsureDeleted();
        Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = default);

        void SetIsolationLevel(IsolationLevel isolationLvl = IsolationLevel.ReadCommitted);
        Task SetIsolationLevelAsync(IsolationLevel isolationLvl = IsolationLevel.ReadCommitted);

        void EnsureDBCleanState();
        Task EnsureDBCleanStateAsync();

        void EvictChanges();
        bool HasCommittedChanges();

        void SaveChanges();
        Task SaveChangesAsync();

        void BeginTransaction();
        void BeginTransaction(IsolationLevel isolationLevel);

        Task BeginTransactionAsync();
        Task BeginTransactionAsync(IsolationLevel isolationLevel);

        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
