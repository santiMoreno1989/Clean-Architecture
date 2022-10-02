using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.API
{
    public static class Extensions
    {
        public static void MapEndpoints(this WebApplication application) 
        {
            var registeredModules = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsClass && t.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>();

            foreach (var module in registeredModules)
                module.MapEndpoints(application);
                    
        }
    }
}
