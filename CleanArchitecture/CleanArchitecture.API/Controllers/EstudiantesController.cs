using CleanArchitecture.Application.Common.Dtos;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudiantesService _estudiantesService;

        public EstudiantesController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
            => Ok(await _estudiantesService.GetAllStudents());

        [HttpPost]
        public async Task<IActionResult> CreateStudent(EstudianteRequest student)
            => Created("New", await _estudiantesService.CreateStudent(student));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
            => Ok(await _estudiantesService.GetStudent(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _estudiantesService.DeleteStudent(id);
            return Ok("Se elimino el estudiante correctamente.");
        }
    }
}
