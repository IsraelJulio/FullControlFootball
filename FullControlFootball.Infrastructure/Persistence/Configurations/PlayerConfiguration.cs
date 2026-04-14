using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");
        builder.HasKey(x => x.Id);

builder.Property(x => x.Name).HasMaxLength(180).IsRequired();
builder.Property(x => x.KnownAs).HasMaxLength(180);
builder.Property(x => x.PrimaryPosition).HasMaxLength(20).IsRequired();
builder.Property(x => x.SecondaryPositions).HasMaxLength(100);
builder.Property(x => x.PreferredFoot).HasMaxLength(20);
builder.Property(x => x.FaceImageUrl).HasMaxLength(1000);
builder.Property(x => x.ExternalSource).HasMaxLength(100);
builder.Property(x => x.ExternalId).HasMaxLength(150);
builder.HasIndex(x => x.NationalityCountryId);
builder.HasIndex(x => new { x.ExternalSource, x.ExternalId });
builder.HasOne(x => x.NationalityCountry).WithMany().HasForeignKey(x => x.NationalityCountryId).OnDelete(DeleteBehavior.SetNull);

    }
}
