using CleanArchitecture.Application.Common.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IEstudiantesService
    {
        Task<Estudiante> CreateStudent(EstudianteRequest estudiante);
        Task<IEnumerable<EstudianteResponse>> GetAllStudents();
        Task<Estudiante> GetStudent(int id);
    }
}