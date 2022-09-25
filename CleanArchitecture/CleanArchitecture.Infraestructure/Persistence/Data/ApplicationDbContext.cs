using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }
    }
}
