using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.ToTable("clubs");
        builder.HasKey(x => x.Id);

builder.Property(x => x.Name).HasMaxLength(180).IsRequired();
builder.Property(x => x.ShortName).HasMaxLength(50);
builder.Property(x => x.LogoUrl).HasMaxLength(1000);
builder.Property(x => x.PrimaryColor).HasMaxLength(20);
builder.Property(x => x.SecondaryColor).HasMaxLength(20);
builder.Property(x => x.ExternalSource).HasMaxLength(100);
builder.Property(x => x.ExternalId).HasMaxLength(150);
builder.HasIndex(x => x.CountryId);
builder.HasIndex(x => x.DefaultCompetitionId);
builder.HasIndex(x => new { x.ExternalSource, x.ExternalId });
builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.SetNull);
builder.HasOne(x => x.DefaultCompetition).WithMany().HasForeignKey(x => x.DefaultCompetitionId).OnDelete(DeleteBehavior.SetNull);

    }
}
