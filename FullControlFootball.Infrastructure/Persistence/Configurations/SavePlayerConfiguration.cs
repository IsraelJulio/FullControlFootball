using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class SavePlayerConfiguration : IEntityTypeConfiguration<SavePlayer>
{
    public void Configure(EntityTypeBuilder<SavePlayer> builder)
    {
        builder.ToTable("save_players");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlayerNameSnapshot).HasMaxLength(FieldLengths.SnapshotName).IsRequired();
        builder.Property(x => x.PrimaryPositionSnapshot).HasMaxLength(FieldLengths.PositionCode);

        builder.HasIndex(x => x.CareerSaveId);
        builder.HasIndex(x => x.PlayerId);
        builder.HasIndex(x => x.CurrentClubId);
        builder.HasIndex(x => new { x.CareerSaveId, x.PlayerId }).IsUnique();

        builder.HasOne(x => x.CareerSave)
            .WithMany(x => x.SavePlayers)
            .HasForeignKey(x => x.CareerSaveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CurrentClub)
            .WithMany()
            .HasForeignKey(x => x.CurrentClubId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
