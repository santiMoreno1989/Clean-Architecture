using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Persistence.Configurations
{
    public class CursoAsignacionConfigurations : IEntityTypeConfiguration<CursoAsignacion>
    {
        public void Configure(EntityTypeBuilder<CursoAsignacion> builder)
        {
            builder.HasKey(c => new {c.CursoId,c.InstructorId});
        }
    }
}
