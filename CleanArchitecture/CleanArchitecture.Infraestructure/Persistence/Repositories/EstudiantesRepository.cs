using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infraestructure.Persistence.Data;

namespace CleanArchitecture.Infraestructure.Persistence.Repositories
{
    public class EstudiantesRepository : Repository<Estudiante>, IEstudiantesRepository
    {
        public EstudiantesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
