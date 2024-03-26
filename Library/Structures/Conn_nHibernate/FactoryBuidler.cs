using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.Configuration;
using NHibernate;
using System.Reflection;

namespace Structures.Conn_nHibernate
{
    public class FactoryBuilder : IFactoryBuilder
    {
        private static string _assemblyMapping { get; } = "Layer";
        private IDictionary<string, string> _configs { get; set; }
        private ISessionFactory _sessionFactory { get; set; }

        public FactoryBuilder(IConfigurationSection configs, IConfigurationSection connectionStrings)
        {
            _configs = configs.GetChildren().ToDictionary(xKey => xKey.Key, xVal => xVal.Value);

            if (_configs.ContainsKey(NHibernate.Cfg.Environment.ConnectionStringName)) {
                var connectionStringName = _configs[NHibernate.Cfg.Environment.ConnectionStringName];
                if(connectionStringName != null || connectionStringName != string.Empty) {
                    _configs[NHibernate.Cfg.Environment.ConnectionString] = connectionStrings.GetSection(connectionStringName).Value;
                }
            }
        }

        public ISessionFactory GetFactory()
        {
            lock (this)
                _sessionFactory ??= SessionBuilder(_configs);

            return _sessionFactory;
        }

        private static ISessionFactory SessionBuilder(IDictionary<string, string> configs)
        {
            try {
                var config = new NHibernate.Cfg.Configuration() { Properties = configs };   

                return Fluently.Configure(config).Mappings(map => {
                    map.FluentMappings.Conventions.Setup(s => s.Add(AutoImport.Never()));
                    map.FluentMappings.AddFromAssembly(Assembly.Load(_assemblyMapping));
                }).BuildSessionFactory();
            } catch (Exception ex) {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
