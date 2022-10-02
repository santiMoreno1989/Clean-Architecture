using CleanArchitecture.Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(options=>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), optionsBuilder=>
                    optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);
            
            services.Scan(selector=>
                selector.FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(filter=> filter.Where(type=> type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            
            return services;
        }
    }
}
