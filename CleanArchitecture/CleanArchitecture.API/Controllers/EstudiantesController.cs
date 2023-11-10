using CleanArchitecture.Application.Common.Dtos;
using CleanArchitecture.Application.Common.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudiantesService _estudiantesService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

		public EstudiantesController(IEstudiantesService estudiantesService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
		{
			_estudiantesService = estudiantesService;
			_backgroundJobClient = backgroundJobClient;
			_recurringJobManager = recurringJobManager;
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

		[HttpGet]
		[Route("{id}/fireAndForgetJob")]
		public IActionResult GetStudentJob(int id)
		{
			_backgroundJobClient.Enqueue(() => _estudiantesService.GetStudent(id));
			return Ok();
		}

        [HttpGet]
        [Route("DelayedJob")]
		public IActionResult ConsoleMessage() 
        {
            _backgroundJobClient.Schedule(() => Console.WriteLine("********* Job with 5 sec delay... **********"), TimeSpan.FromSeconds(5));
            return Ok();
        }

		[HttpDelete]
		[Route("RecurringJob")]
		public IActionResult DeleteDuplicateStudent()
		{
			_recurringJobManager.AddOrUpdate("DeleteInactiveStudent", () => _estudiantesService.DeleteStudentJob(), "*/3 * * * * ");
			return NoContent();
		}

		[HttpPost]
        [Route("ContinuationJob")]
		public IActionResult CreateStudentJob() 
        {
            var jobId = _backgroundJobClient.Enqueue(() => _estudiantesService.CreateStudentJob());
            _backgroundJobClient.ContinueJobWith(jobId,() => Console.WriteLine($"Se proceso con exito la primer tarea con ID {jobId}"));
            return Ok();
        }

    }
}
