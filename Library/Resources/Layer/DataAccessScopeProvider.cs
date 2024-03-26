using Layer.Core.Repositories.Common;
using Layer.Core.Repositories.Membership;
using Layer.Domain.IRepositories.Common;
using Layer.Domain.IRepositories.Membership;
using Microsoft.Extensions.DependencyInjection;

namespace Layer
{
    public static class DataAccecssScopeProvider
    {
        public static void DataAccessServices(this IServiceCollection services)
        {
            // Membership
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // common
            services.AddTransient<IProductRepository, ProductRepository>();
        }


        public static void DataAccessServicesBuilder(this IServiceCollection services)
        {
            var integrationProviderTypes = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains("Layer"));

            // TODO: Need to check if the 2 class extend from same interface. Auto builder.
            foreach (var intfc in integrationProviderTypes.Where(t => t.IsInterface)) {
                var implementation = integrationProviderTypes.FirstOrDefault(c => c.IsClass && intfc.IsAssignableFrom(c) && !c.IsAbstract);
                if (implementation != null)
                    services.AddTransient(intfc, implementation);
            }
        }
    }
}
