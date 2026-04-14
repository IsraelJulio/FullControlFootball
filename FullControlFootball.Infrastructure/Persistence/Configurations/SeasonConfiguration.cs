using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable("seasons");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Label).HasMaxLength(FieldLengths.Label).IsRequired();

        builder.HasIndex(x => x.CareerSaveId);
        builder.HasIndex(x => new { x.CareerSaveId, x.Number }).IsUnique();

        builder.HasOne(x => x.CareerSave)
            .WithMany(x => x.Seasons)
            .HasForeignKey(x => x.CareerSaveId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
