using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class CompetitionStandingConfiguration : IEntityTypeConfiguration<CompetitionStanding>
{
    public void Configure(EntityTypeBuilder<CompetitionStanding> builder)
    {
        builder.ToTable("competition_standings");
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.SeasonCompetitionId);
        builder.HasIndex(x => new { x.SeasonCompetitionId, x.SnapshotDateUtc });

        builder.HasOne(x => x.SeasonCompetition)
            .WithMany(x => x.Standings)
            .HasForeignKey(x => x.SeasonCompetitionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
