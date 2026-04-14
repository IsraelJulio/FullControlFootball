using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class SaveClubConfiguration : IEntityTypeConfiguration<SaveClub>
{
    public void Configure(EntityTypeBuilder<SaveClub> builder)
    {
        builder.ToTable("save_clubs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClubNameSnapshot).HasMaxLength(FieldLengths.ClubName).IsRequired();

        builder.HasIndex(x => x.CareerSaveId);
        builder.HasIndex(x => x.ClubId);
        builder.HasIndex(x => new { x.CareerSaveId, x.ClubId }).IsUnique();

        builder.HasOne(x => x.CareerSave)
            .WithMany(x => x.SaveClubs)
            .HasForeignKey(x => x.CareerSaveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Club)
            .WithMany()
            .HasForeignKey(x => x.ClubId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
