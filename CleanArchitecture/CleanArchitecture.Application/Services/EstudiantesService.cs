using CleanArchitecture.Application.Common.Dtos;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Services
{
    public class EstudiantesService : IEstudiantesService
    {
        private readonly IEstudiantesRepository _repository;
        private readonly HttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        public EstudiantesService(IEstudiantesRepository repository, IConfiguration configuration, HttpClientService httpClientService)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClientService = httpClientService;
        }

        public async Task<IEnumerable<EstudianteResponse>> GetAllStudents()
        {
            var estudiantes = (await _repository.GetAll()).Select(e=>(EstudianteResponse) e);

            if (!estudiantes.Any())
                throw new KeyNotFoundException("No existen estudiantes en la base de datos.");

            return estudiantes;

        }

        public async Task<Estudiante> CreateStudent(EstudianteRequest estudiante)
        {
            var estudianteExiste = await _repository.GetAll();
            var nombre = estudianteExiste.Select(e => e.Nombre.ToLower());
            var apellido = estudianteExiste.Select(e => e.Apellido.ToLower());

            var student = new Estudiante()
            {
                Apellido = estudiante.Apellido,
                FechaInscripcion = DateTime.Now,
                Nombre = estudiante.Nombre,
            };

            if (nombre.Contains(student.Nombre.ToLower()) && apellido.Contains(student.Apellido.ToLower()))
                throw new BadRequestException($"El estudiante {student.Nombre} {student.Apellido} ya existe en la base de datos.");

            await _repository.Add(student);
            return student;
        }

        public async Task<Estudiante> GetStudent(int id)
        {
            if (id == 0)
                throw new BadRequestException();

            var estudiante = await _repository.GetById(id);

            if (estudiante == null)
                throw new KeyNotFoundException($"El estudiante con ID : {id} no existe.");

            return estudiante;
        }
    }
}
