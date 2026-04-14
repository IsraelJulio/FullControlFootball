using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");
        builder.HasKey(x => x.Id);

builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
builder.HasIndex(x => x.Name).IsUnique();
builder.HasIndex(x => x.Code).IsUnique();

    }
}
