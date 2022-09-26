using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Persistence.Configurations
{
    public class DepartamentoConfigurations : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.Property(d => d.Nombre).HasMaxLength(70);
            builder.Property(d => d.Presupuesto).HasColumnType("money");
        }
    }
}
