using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) 
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {

                var response = context.Response;
                context.Response.ContentType = "application/json";


                switch (error)
                {
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                ProblemDetails problemDetails = new()
                {
                    Title = "Se produjo un error al procesar su consulta.",
                    Status = response.StatusCode,
                    Detail = error?.Message,
                    Instance = error?.StackTrace
                };

                var jsonResult = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(jsonResult);
                _logger.LogError(jsonResult);
            }
        }
    }
}
