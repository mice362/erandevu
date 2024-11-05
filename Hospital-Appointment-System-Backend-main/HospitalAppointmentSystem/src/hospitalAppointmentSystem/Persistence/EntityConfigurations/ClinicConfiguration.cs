using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.ToTable("Clinics");
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Phone).IsRequired();
        builder.Property(c => c.Address);
        builder.Property(c => c.Email);
        builder.Property(c => c.About);

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
    }
}
