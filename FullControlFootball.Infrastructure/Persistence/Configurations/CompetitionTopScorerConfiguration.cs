using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class CompetitionTopScorerConfiguration : IEntityTypeConfiguration<CompetitionTopScorer>
{
    public void Configure(EntityTypeBuilder<CompetitionTopScorer> builder)
    {
        builder.ToTable("competition_top_scorers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlayerNameSnapshot).HasMaxLength(FieldLengths.SnapshotName).IsRequired();
        builder.Property(x => x.ClubNameSnapshot).HasMaxLength(FieldLengths.SnapshotName);

        builder.HasIndex(x => x.SeasonCompetitionId);
        builder.HasIndex(x => new { x.SeasonCompetitionId, x.SnapshotDateUtc, x.Position });

        builder.HasOne(x => x.SeasonCompetition).WithMany(x => x.TopScorers).HasForeignKey(x => x.SeasonCompetitionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.SavePlayer).WithMany().HasForeignKey(x => x.SavePlayerId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Player).WithMany().HasForeignKey(x => x.PlayerId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.SaveClub).WithMany().HasForeignKey(x => x.SaveClubId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Club).WithMany().HasForeignKey(x => x.ClubId).OnDelete(DeleteBehavior.SetNull);
    }
}
