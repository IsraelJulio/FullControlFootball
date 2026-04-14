using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class CompetitionStandingRowConfiguration : IEntityTypeConfiguration<CompetitionStandingRow>
{
    public void Configure(EntityTypeBuilder<CompetitionStandingRow> builder)
    {
        builder.ToTable("competition_standing_rows");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClubNameSnapshot).HasMaxLength(FieldLengths.SnapshotName).IsRequired();

        builder.HasIndex(x => x.CompetitionStandingId);
        builder.HasIndex(x => x.SaveClubId);
        builder.HasIndex(x => x.ClubId);
        builder.HasIndex(x => new { x.CompetitionStandingId, x.Position }).IsUnique();

        builder.HasOne(x => x.CompetitionStanding)
            .WithMany(x => x.Rows)
            .HasForeignKey(x => x.CompetitionStandingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.SaveClub)
            .WithMany()
            .HasForeignKey(x => x.SaveClubId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Club)
            .WithMany()
            .HasForeignKey(x => x.ClubId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
