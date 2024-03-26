using NHibernate;

namespace Structures.Conn_nHibernate
{
    public interface IFactoryBuilder
    {
        ISessionFactory GetFactory();
    }
}
