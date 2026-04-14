using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.ToTable("competitions");
        builder.HasKey(x => x.Id);

builder.Property(x => x.Name).HasMaxLength(180).IsRequired();
builder.Property(x => x.ExternalSource).HasMaxLength(100);
builder.Property(x => x.ExternalId).HasMaxLength(150);
builder.HasIndex(x => x.CountryId);
builder.HasIndex(x => new { x.ExternalSource, x.ExternalId });
builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.SetNull);

    }
}
