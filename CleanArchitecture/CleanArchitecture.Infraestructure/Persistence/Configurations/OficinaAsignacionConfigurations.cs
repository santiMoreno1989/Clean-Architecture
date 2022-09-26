using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Persistence.Configurations
{
    public class OficinaAsignacionConfigurations : IEntityTypeConfiguration<OficinaAsignacion>
    {
        public void Configure(EntityTypeBuilder<OficinaAsignacion> builder)
        {
            builder.HasKey(o => o.InstructorID);
            builder.Property(o => o.Ubicacion).HasMaxLength(70);
        }
    }
}
