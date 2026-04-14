using FullControlFootball.Domain.Common;
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

        builder.Property(x => x.Name).HasMaxLength(FieldLengths.SnapshotName).IsRequired();
        builder.Property(x => x.KnownAs).HasMaxLength(FieldLengths.SnapshotName);
        builder.Property(x => x.PrimaryPosition).HasMaxLength(FieldLengths.PositionCode).IsRequired();
        builder.Property(x => x.SecondaryPositions).HasMaxLength(FieldLengths.SecondaryPositions);
        builder.Property(x => x.PreferredFoot).HasMaxLength(FieldLengths.PositionCode);
        builder.Property(x => x.FaceImageUrl).HasMaxLength(FieldLengths.Url);
        builder.Property(x => x.ExternalSource).HasMaxLength(FieldLengths.ExternalSource);
        builder.Property(x => x.ExternalId).HasMaxLength(FieldLengths.ExternalId);

        builder.HasIndex(x => x.NationalityCountryId);
        builder.HasIndex(x => new { x.ExternalSource, x.ExternalId });

        builder.HasOne(x => x.NationalityCountry)
            .WithMany()
            .HasForeignKey(x => x.NationalityCountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
