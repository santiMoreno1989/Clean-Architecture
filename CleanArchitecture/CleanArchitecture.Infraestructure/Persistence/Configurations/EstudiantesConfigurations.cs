using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Persistence.Configurations
{
    public class EstudiantesConfigurations : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nombre).HasMaxLength(150);
            builder.Property(e => e.Apellido).HasMaxLength(200);
        }
    }
}
