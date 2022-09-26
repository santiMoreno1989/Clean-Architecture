using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly EstudiantesService _estudiantesService;

        public EstudiantesController(EstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentsList() => Ok(await _estudiantesService.GetAllStudents());

        [HttpPost]
        public void CreateStudent(Estudiante estudiante) => Created("New Student",_estudiantesService.CreateStudent(estudiante));
    }
}
