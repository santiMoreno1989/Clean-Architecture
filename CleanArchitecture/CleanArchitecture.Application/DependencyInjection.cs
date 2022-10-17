using CleanArchitecture.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        private const string service = "Service";
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services) 
        {
            services.AddTransient<HttpClientService>();
            services.Scan(selector => 
                selector.FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(filter=> filter.Where(type=> type.Name.EndsWith(service)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    );
            return services;
        }
    }
}
