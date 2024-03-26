using EFLayer.Core.Repositories.Common;
using EFLayer.Domain.IRepositories.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer
{
    public static class EFDataAccecssScopeProvider
    {
        public static void EFDataAccessServices(this IServiceCollection services)
        {
            // common
            services.AddTransient<IEFProductRepository, EFProductRepository>();
        }


        public static void EFDataAccessServicesBuilder(this IServiceCollection services)
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
