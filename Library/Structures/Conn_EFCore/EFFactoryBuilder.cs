using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Conn_EFCore
{
    public class EFFactoryBuilder<T> : IDisposable
    {
        private bool _disposed;

        private Func<T> _instanceFunc;

        private DbContext _dbContext;

        public DbContext DbContext => _dbContext ??= _instanceFunc.Invoke() as DbContext;

        public EFFactoryBuilder(Func<T> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null) {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
