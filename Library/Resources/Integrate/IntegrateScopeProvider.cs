using Integrate.IServices.Common;
using Integrate.IServices.Membership;
using Integrate.Services.Common;
using Integrate.Services.Membership;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrate
{
    public static class IntegrateScopeProvider
    {
        public static void IntegrationServices(this IServiceCollection services)
        {
            // membership
            services.AddTransient<IUserService, UserService>();

            // common
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IEFProductService, EFProductService>();
        }

        public static void IntegrationServicesBuilder(this IServiceCollection services)
        {
            var integrationProviderTypes = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains("sc.Integration"));

            // TODO: Need to check if the 2 class extend from same interface. Auto builder.
            foreach (var intfc in integrationProviderTypes.Where(t => t.IsInterface)) {
                var implementation = integrationProviderTypes.FirstOrDefault(c => c.IsClass && intfc.IsAssignableFrom(c) && !c.IsAbstract);
                if (implementation != null)
                    services.AddTransient(intfc, implementation);
            }
        }
    }
}
