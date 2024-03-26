using Layer;
using Integrate;
using EFLayer;
using Layer.Domain.GenericModels.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Structures.Conn_nHibernate;
using Tools.DataAccess;
using Microsoft.Win32.SafeHandles;
using Microsoft.EntityFrameworkCore;
using EFLayer.Domain;
using Structures.Conn_EFCore;

namespace WebAPIs
{
    public static class ServiceExtension
    {

        /// <summary>
        /// Config file AppSettings.json bind
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <remarks>
        /// 05/05/2023, created by Orion
        /// </remarks>
        public static void ConfigureConfigFiles(this IServiceCollection services, IConfiguration configuration)
        {
            var appSetting = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSetting);
            services.AddSingleton(appSetting);
        }

        /// <summary>
        /// Custom Dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <remarks>
        /// 05/05/2023, created by Orion
        /// </remarks>
        public static void ConfigureCustomDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection NhibernateConfiguration = configuration.GetSection("NhibernateConfiguration");
            IConfigurationSection ConnectionStrings = configuration.GetSection("ConnectionStrings");

            services.AddSingleton<IFactoryBuilder>(x => new FactoryBuilder(NhibernateConfiguration.GetSection("DefaultConnConfiguration"), ConnectionStrings));
            services.AddScoped<IAppSessionBuilder>(x => new AppSessionBuilder(x.GetRequiredService<IFactoryBuilder>()));
            services.AddScoped<IUnitOfWork>(x => new UnitOfWork(x.GetRequiredService<ILogger<UnitOfWork>>(), x.GetRequiredService<IAppSessionBuilder>()));

            // EF
            // Configure DbContext with Scoped lifetime
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConn"));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            services.AddScoped<EFFactoryBuilder<AppDbContext>>();
            services.AddScoped<IEFUnitOfWork, EFUnitOfWork<AppDbContext>>();

            services.AddScoped(typeof(IEFRepository<object>), typeof(EFRepoBase<object, AppDbContext>));

            services.DataAccessServices();
            services.IntegrationServices();

            services.EFDataAccessServices();
        }

    }
}
