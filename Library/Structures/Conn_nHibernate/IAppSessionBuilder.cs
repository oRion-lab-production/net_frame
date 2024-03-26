using NHibernate;

namespace Structures.Conn_nHibernate
{
    public interface IAppSessionBuilder
    {
        ISession GetSession();
    }
}
