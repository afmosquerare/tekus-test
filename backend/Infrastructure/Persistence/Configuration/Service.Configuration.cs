
using Domain.Aggregates.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;


public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(s => new { s.Id, s.Name }).IsUnique();


        builder.Property(s => s.HourlyRate)
            .HasPrecision(10, 2);

        builder.OwnsMany(s => s.Countries, c =>
        {
            c.ToTable("ServiceCountries");

            c.WithOwner().HasForeignKey("ServiceId");

            c.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            c.Property(x => x.Code)
                .HasColumnName("Code")
                .IsRequired();


            c.HasKey("ServiceId", "Code");
        });
    }


}
