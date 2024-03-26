
namespace Tools.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void BeginTransaction(System.Data.IsolationLevel isolationLevel);
        void Flush();
        void Clear();
        void Close();

        void Commit();

        void RollBack();

        Task FlushAsync();

        Task CommitAsync();

        Task RollBackAsync();

        Task FlushAsync(CancellationToken cancellationToken);

        Task CommitAsync(CancellationToken cancellationToken);

        Task RollBackAsync(CancellationToken cancellationToken);
    }
}
