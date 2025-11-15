using Domain.Aggregates.Providers;
using Domain.Aggregates.Providers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;


public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("Providers");
        builder.HasKey(c => c.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Nit).IsRequired().HasMaxLength(9);
        builder.HasIndex(p => p.Nit).IsUnique();
        builder.Property(p => p.Email)
            .HasConversion( email => email.Value, value => Email.Create(value)!)
            .HasMaxLength(255);
        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasMany(p => p.Services)
            .WithOne()
            .HasForeignKey("ProviderId");

        builder.OwnsMany(p => p.CustomFields, cf =>
        {
            cf.ToTable("CustomFields");
            cf.WithOwner().HasForeignKey("ProviderId");
            cf.Property(c => c.FieldName).IsRequired();
            cf.Property(c => c.FieldValue).IsRequired();
            cf.HasKey("ProviderId", "FieldName");
        });

        builder.Property(p => p.IsActive).IsRequired();

    }


}
