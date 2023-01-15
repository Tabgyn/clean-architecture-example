using CleanArchitectureExample.Domain.Common.ValueObjects;
using CleanArchitectureExample.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureExample.Persistence.Configurations;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder
            .HasKey(x => x.Id)
            .HasName("CustomerId");

        builder
            .Property(x => x.Id)
            .HasConversion(
            x => x.Value, v => v);

        builder
            .Property(x => x.Email)
            .HasConversion(
            x => x.Address,
            v => Email.Create(v).Address);

        builder
            .Property(x => x.FirstName)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(x => x.LastName)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(x => x.DateOfBirth)
            .HasColumnType("date")
            .IsRequired(true);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
