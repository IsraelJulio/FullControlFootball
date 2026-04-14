using FullControlFootball.Domain.Common;
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

        builder.Property(x => x.Name).HasMaxLength(FieldLengths.SnapshotName).IsRequired();
        builder.Property(x => x.ExternalSource).HasMaxLength(FieldLengths.ExternalSource);
        builder.Property(x => x.ExternalId).HasMaxLength(FieldLengths.ExternalId);

        builder.HasIndex(x => x.CountryId);
        builder.HasIndex(x => new { x.ExternalSource, x.ExternalId });

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
