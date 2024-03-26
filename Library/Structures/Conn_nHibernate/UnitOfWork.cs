using Microsoft.Extensions.Logging;
using NHibernate;
using Tools.DataAccess;

namespace Structures.Conn_nHibernate
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ILogger<UnitOfWork> _logger { get; }

        private IAppSessionBuilder _sessionBuilder { get; }
        private ISession _session { get; set; }

        public UnitOfWork(ILogger<UnitOfWork> logger, IAppSessionBuilder sessionBuilder)
        {
            _logger = logger;

            _sessionBuilder = sessionBuilder;

            if (_sessionBuilder != null)
                _session = _sessionBuilder.GetSession();
        }

        private ITransaction GetTransaction() => _session.GetCurrentTransaction();

        public void BeginTransaction()
        {
            var transaction = GetTransaction();

            if (transaction != null)
                if (transaction.IsActive || transaction.WasCommitted || transaction.WasRolledBack)
                    transaction.Dispose();

            _session.BeginTransaction();
        }

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel)
        {
            var transaction = GetTransaction();

            if (transaction != null)
                if (transaction.IsActive || transaction.WasCommitted || transaction.WasRolledBack)
                    transaction.Dispose();

            _session.BeginTransaction(isolationLevel);
        }

        public void Flush() => _session.Flush();
        public void Clear() => _session.Clear();
        public void Close() => _session.Close();

        public void Commit()
        {
            var transaction = GetTransaction();

            if (transaction == null)
                throw new InvalidOperationException("Transaction null, please call begin transaction through BeginTransaction()");

            if (!transaction.IsActive)
                throw new InvalidOperationException("No transaction active, please call begin transaction through BeginTransaction()");

            if (transaction.WasCommitted || transaction.WasRolledBack)
                throw new InvalidOperationException("Transaction has performed, please call begin transaction through BeginTransaction()");

            transaction.Commit();
        }

        public void RollBack()
        {
            var transaction = GetTransaction();

            if (transaction != null ? transaction.IsActive : false)
                transaction.Rollback();
        }
        public async Task FlushAsync() => await _session.FlushAsync();

        public async Task CommitAsync()
        {
            var transaction = GetTransaction();

            if (transaction == null)
                throw new InvalidOperationException("Transaction null, please call begin transaction through BeginTransaction()");

            if (!transaction.IsActive)
                throw new InvalidOperationException("No transaction active, please call begin transaction through BeginTransaction()");

            if (transaction.WasCommitted || transaction.WasRolledBack)
                throw new InvalidOperationException("Transaction has performed, please call begin transaction through BeginTransaction()");

            await transaction.CommitAsync();
        }

        public async Task RollBackAsync()
        {
            var transaction = GetTransaction();

            if (transaction != null ? transaction.IsActive : false)
                await transaction.RollbackAsync();
        }

        public async Task FlushAsync(CancellationToken cancellationToken) => await _session.FlushAsync(cancellationToken);

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            var transaction = GetTransaction();

            if (transaction == null)
                throw new InvalidOperationException("Transaction null, please call begin transaction through BeginTransaction()");

            if (!transaction.IsActive)
                throw new InvalidOperationException("No transaction active, please call begin transaction through BeginTransaction()");

            if (transaction.WasCommitted || transaction.WasRolledBack)
                throw new InvalidOperationException("Transaction has performed, please call begin transaction through BeginTransaction()");

            await transaction.CommitAsync(cancellationToken);
        }

        public async Task RollBackAsync(CancellationToken cancellationToken)
        {
            var transaction = GetTransaction();

            if (transaction != null ? transaction.IsActive : false)
                await transaction.RollbackAsync(cancellationToken);
        }

        public void Dispose()
        {
            var transaction = GetTransaction();

            if (transaction != null)
                transaction.Dispose();

            if (_session != null) {
                Clear();
                Close();
            }
        }
    }
}
