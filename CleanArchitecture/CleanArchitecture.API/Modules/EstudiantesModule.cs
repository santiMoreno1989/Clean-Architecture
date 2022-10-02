using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Modules
{
    public class EstudiantesModule : IModule
    {
        public void MapEndpoints(object endpoints)
        {
            var endpoint = (IEndpointRouteBuilder)endpoints;
            const string route = "api/estudiantes";
            const string tag = "estudiantes";

            endpoint.MapGet($"{route}",
                async ([FromServices]IEstudiantesService service)=> await service.GetAllStudents()).WithTags(tag);
        }
    }
}
