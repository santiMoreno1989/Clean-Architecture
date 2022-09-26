using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infraestructure.Persistence.Repositories;

namespace CleanArchitecture.Application.Services
{
    public class EstudiantesService
    {
        private readonly IRepository<Estudiante> _repository;

        public EstudiantesService(Infraestructure.Persistence.Repositories.IRepository<Estudiante> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Estudiante>> GetAllStudents()
            =>await _repository.GetAll();

        public async Task CreateStudent(Estudiante estudiante) 
        {
            var student = new Estudiante() 
            {
                 Apellido = estudiante.Apellido,
                 FechaInscripcion=estudiante.FechaInscripcion,
                 Nombre=estudiante.Nombre,
            };
            _repository.Add(student);
        }
    }
}
