namespace CleanArchitecture.API.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddleware(this IApplicationBuilder builder) 
        {
            builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
