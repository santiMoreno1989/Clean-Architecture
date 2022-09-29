using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IEstudiantesService
    {
        Task CreateStudent(Estudiante estudiante);
        Task<IEnumerable<Estudiante>> GetAllStudents();
    }
}