using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class SeasonCompetitionConfiguration : IEntityTypeConfiguration<SeasonCompetition>
{
    public void Configure(EntityTypeBuilder<SeasonCompetition> builder)
    {
        builder.ToTable("season_competitions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameSnapshot).HasMaxLength(FieldLengths.SnapshotName).IsRequired();

        builder.HasIndex(x => x.CareerSaveId);
        builder.HasIndex(x => x.SeasonId);
        builder.HasIndex(x => x.CompetitionId);
        builder.HasIndex(x => new { x.SeasonId, x.CompetitionId }).IsUnique();

        builder.HasOne(x => x.CareerSave)
            .WithMany(x => x.SeasonCompetitions)
            .HasForeignKey(x => x.CareerSaveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Season)
            .WithMany(x => x.SeasonCompetitions)
            .HasForeignKey(x => x.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Competition)
            .WithMany()
            .HasForeignKey(x => x.CompetitionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
