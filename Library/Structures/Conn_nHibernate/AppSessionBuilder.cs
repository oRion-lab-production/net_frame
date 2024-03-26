using NHibernate;

namespace Structures.Conn_nHibernate
{
    public class AppSessionBuilder : IAppSessionBuilder, IDisposable
    {
        private IFactoryBuilder _factoryBuilder { get; }
        private ISession _session { get; set; }

        public AppSessionBuilder(IFactoryBuilder factoryBuilder)
        {
            _factoryBuilder = factoryBuilder;
        }

        public ISession GetSession()
        {
            return BuildSession();
        }

        private ISession BuildSession()
        {
            if (_session != null)
                return _session;

            _session = _factoryBuilder.GetFactory().OpenSession();
            _session.FlushMode = FlushMode.Commit;

            return _session;
        }

        public void Dispose()
        {
            if (_session != null)
                _session.Dispose();
        }
    }
}
