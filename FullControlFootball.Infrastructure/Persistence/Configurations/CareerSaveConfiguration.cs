using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class CareerSaveConfiguration : IEntityTypeConfiguration<CareerSave>
{
    public void Configure(EntityTypeBuilder<CareerSave> builder)
    {
        builder.ToTable("career_saves");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(FieldLengths.Name).IsRequired();
        builder.Property(x => x.GameEdition).HasMaxLength(FieldLengths.GameEdition).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(FieldLengths.Description);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.Name });

        builder.HasOne(x => x.User)
            .WithMany(x => x.CareerSaves)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.MainClub)
            .WithMany()
            .HasForeignKey(x => x.MainClubId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
