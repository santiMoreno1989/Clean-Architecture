using CleanArchitecture.Application.Common.Dtos;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IEstudiantesService
    {
        Task<Estudiante> CreateStudent(EstudianteRequest estudiante);
        Task<IEnumerable<Estudiante>> GetAllStudents();
        Task<Estudiante> GetStudent(int id);
        Task DeleteStudent(int id);
    }
}