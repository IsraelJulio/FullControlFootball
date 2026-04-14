using FullControlFootball.Domain.Common;
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

        builder.Property(x => x.Name).HasMaxLength(FieldLengths.SnapshotName).IsRequired();
        builder.Property(x => x.ShortName).HasMaxLength(FieldLengths.ShortName);
        builder.Property(x => x.LogoUrl).HasMaxLength(FieldLengths.Url);
        builder.Property(x => x.PrimaryColor).HasMaxLength(FieldLengths.Color);
        builder.Property(x => x.SecondaryColor).HasMaxLength(FieldLengths.Color);
        builder.Property(x => x.ExternalSource).HasMaxLength(FieldLengths.ExternalSource);
        builder.Property(x => x.ExternalId).HasMaxLength(FieldLengths.ExternalId);

        builder.HasIndex(x => x.CountryId);
        builder.HasIndex(x => x.DefaultCompetitionId);
        builder.HasIndex(x => new { x.ExternalSource, x.ExternalId });

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.DefaultCompetition)
            .WithMany()
            .HasForeignKey(x => x.DefaultCompetitionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
