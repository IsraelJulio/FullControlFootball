using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class ClubPlayerConfiguration : IEntityTypeConfiguration<ClubPlayer>
{
    public void Configure(EntityTypeBuilder<ClubPlayer> builder)
    {
        builder.ToTable("club_players");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SquadRole).HasMaxLength(FieldLengths.SquadRole);

        builder.HasIndex(x => x.ClubId);
        builder.HasIndex(x => x.PlayerId);
        builder.HasIndex(x => new { x.ClubId, x.PlayerId, x.IsActive });

        builder.HasOne(x => x.Club)
            .WithMany()
            .HasForeignKey(x => x.ClubId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
