using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services
{
    public class EstudiantesService : IEstudiantesService
    {
        private readonly IEstudiantesRepository _repository;

        public EstudiantesService(IEstudiantesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Estudiante>> GetAllStudents()
            => await _repository.GetAll();

        public async Task CreateStudent(Estudiante estudiante)
        {
            var student = new Estudiante()
            {
                Apellido = estudiante.Apellido,
                FechaInscripcion = estudiante.FechaInscripcion,
                Nombre = estudiante.Nombre,
            };
            _repository.Add(student);
        }
    }
}
