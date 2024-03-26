 Application Layer
======================

This documentation for rnd_app application layer, a simple technical specification. The design still in fresh, if you have any comments or ideas, feel free to open dicussion on it. TQ.

## Specimen
 - Please install .net 6 for env use
 - Postmen for testing purposes... 
 - Calling api ``` https://<domain/localhost/ip address>/api/[controller]/[action]/{?id} ```

### 3rd Party Library use
 - log4net [https://logging.apache.org/log4net/]
 - Entity Framework Core [https://www.learnentityframeworkcore.com/]
 - Nhibernate [https://nhibernate.info/doc/]
 - Microsoft Dependency iInjection [https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage]
 - Fluent Migrator [https://fluentmigrator.github.io/]


![](/applicationLayer_flow.png)

### log4net log
 - auto generate 5 file of log under /appData/
	- log.txt
	- ef_log.txt
	- ef_sql_log.txt
	- nhib_log.txt
	- nhib_sql_log.txt

## Installation
### If you doesn't want to run migrator just run "vcs_db.sql"
### Migrator
 - Please run ```dotnet tool install -g FluentMigrator.DotNet.Cli``` in command prompt where the cd location is the solution itself
 ```
 ...\rnd_app>dotnet tool install -g FluentMigrator.DotNet.Cli
 ```
 - pls build a dll first before running
 - pls create a schema "vcs_db" or any prefered name in sql server.
 - Migrator, pls run command below, with changing the connection string
    - Migration
    ```
    ...\rnd_app>dotnet-fm migrate -a ".\Library\Resources\Databases\bin\Debug\net6.0\Databases.dll" -c "Data Source=192.168.1.193;Database=vcs_db;User ID=sa; Password=Sa123;" -p SqlServer2016 --allowDirtyAssemblies -t DefaultDB -t Migration
    ```
    - Seeder (data)
    ```
    ...\rnd_app>dotnet-fm migrate -a ".\Library\Resources\Databases\bin\Debug\net6.0\Databases.dll" -c "Data Source=192.168.1.193;Database=vcs_db;User ID=sa; Password=Sa123;" -p SqlServer2016 --allowDirtyAssemblies -t DefaultDB -t Seeder
    ```
 - Preview SQL 
    - Migration
    ```
    ...\rnd_app>dotnet-fm migrate -a ".\Library\Resources\Databases\bin\Debug\net6.0\Databases.dll" -c "Data Source=192.168.1.193;Database=vcs_db;User ID=sa; Password=Sa123;" -p SqlServer2016 --allowDirtyAssemblies -t DefaultDB -t Migration --no-connection --preview -o=sql.txt
    ```
    - Seeder
    ```
    ...\rnd_app>dotnet-fm migrate -a ".\Library\Resources\Databases\bin\Debug\net6.0\Databases.dll" -c "Data Source=192.168.1.193;Database=vcs_db;User ID=sa; Password=Sa123;" -p SqlServer2016 --allowDirtyAssemblies -t DefaultDB -t Seeder --no-connection --preview -o=sql.txt
    ```

### appsetings.json
 - Pls change the connection of the appsettings.json file
```
{
    "ConnectionStrings": {
        "DefaultConn": "<default connection string>",
        "<add connection for multiple layer connection>": "<connection string>"
    }
}
```
 
 - JWT settings <pls use 128-bit or 16-byte for key [16 character each character consists of 8-bit or 1-byte (0000_0000)]>
```
{
    "JWT": {
        "Issuer": "<issuer link>",
        "Audience": "<audience link>",
        "Key": "+MbQeThWmZq4t7w!"
    }
}
```

### calling the services in program.cs
 - Log4net (References [```using Structures.Trace_Log4net;```])
 - AddLog4net("<config file>", "<config name in xml>")
```
app.Services.GetRequiredService<ILoggerFactory>().AddLog4net("log4net.config", "log4net");
```

 - EF Core Connection (References [```using Structures.Conn_EFCore;```])
 - AppDbContext is just a class in (```EFLayer.Domain```), AppDbContext must extends DbContext class in order to be connected.
 - Why seperate?. Wonder copy and paste the library and call the structures library. Plug & Play.
```
services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(configuration.GetConnectionString("DefaultConn"));
    options.UseLazyLoadingProxies();
});

services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
services.AddScoped<EFFactoryBuilder<AppDbContext>>();
services.AddScoped<IEFUnitOfWork<AppDbContext>, EFUnitOfWork<AppDbContext>>();

services.AddScoped(typeof(IEFRepository<object, AppDbContext>), typeof(EFRepoBase<object, AppDbContext>));
```

 - Nhibernate Connection (References [```using Structures.Conn_nHibernate;```])
 - In order to work u need to change the appsettings.json file for any connection to SQL program
```
{
    "NhibernateConfiguration": {
    "DefaultConnConfiguration": {
      "connection.connection_string_name": "DefaultConn",
      "connection.provider": "NHibernate.Connection.DriverConnectionProvider",
      "dialect": "NHibernate.Dialect.MsSql2012Dialect",
      "connection.driver_class": "NHibernate.Driver.SqlClientDriver",
      "use_proxy_validator": true,
      "adonet.batch_size": 25,
      "generate_statistics": false,
      "current_session_context_class": "async_local",
      "connection.isolation": "ReadCommitted",
      "cache.use_second_level_cache": true,
      "transaction.use_connection_on_system_prepare": false,
      "cache.provider_class": "NHibernate.Cache.HashtableCacheProvider, NHibernate",
      "cache.use_query_cache": false,
      "default_schema": "vcs_db.",
      "show_sql": true
    }
  }
}
```
 - Nhibernate services connection in program.cs
```
IConfigurationSection NhibernateConfiguration = configuration.GetSection("NhibernateConfiguration");
IConfigurationSection ConnectionStrings = configuration.GetSection("ConnectionStrings");

services.AddSingleton<IFactoryBuilder>(x => new FactoryBuilder(NhibernateConfiguration.GetSection("DefaultConnConfiguration"), ConnectionStrings));
services.AddScoped<IAppSessionBuilder>(x => new AppSessionBuilder(x.GetRequiredService<IFactoryBuilder>()));
services.AddScoped<IUnitOfWork>(x => new UnitOfWork(x.GetRequiredService<ILogger<UnitOfWork>>(), x.GetRequiredService<IAppSessionBuilder>()));
```

### Calling the DI for services and repository
 - References for Services [```using Integrate;```]
 - References for EFCore Repository Layer [```using EFLayer;```]
 - References for Nhibernate Repository Layer [```using Layer;```]
```
services.IntegrationServices();
services.EFDataAccessServices();
services.DataAccessServices();
```


## Usage spec
### Data Flow
 - Currently the project are using Service-Oriented Architecture (SOA).
 - TODO: Entity Framework UnitOfWork criteria, lock, SingleOrDefault, Func Linq Queries

![](/applicationLayer_dataflow.png)


### API Request Flow
### API request URL
 - for testing EFCore connection [```https://<<domain/localhost/ip address>>/api/product/get```] (allowannonymous)
 - for register [```https://<<domain/localhost/ip address>>/api/account/register```] (allowannonymous)
 - for login [```https://<<domain/localhost/ip address>>/api/account/login```] (allowannonymous)
 - for product [```https://<<domain/localhost/ip address>>/api/product/(ProcessTable, Create, Read, Update, Delete)```] (Authorize)

### Token JWT
 - Header ```("Authorization", "Bearer {token}")```

![](/applicationLayer_apiRequestFlow.png)

