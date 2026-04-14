using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class TransferWindowConfiguration : IEntityTypeConfiguration<TransferWindow>
{
    public void Configure(EntityTypeBuilder<TransferWindow> builder)
    {
        builder.ToTable("transfer_windows");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(FieldLengths.WindowName).IsRequired();

        builder.HasIndex(x => x.CareerSaveId);
        builder.HasIndex(x => x.SeasonId);
        builder.HasIndex(x => new { x.SeasonId, x.Name }).IsUnique();

        builder.HasOne(x => x.CareerSave)
            .WithMany(x => x.TransferWindows)
            .HasForeignKey(x => x.CareerSaveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Season)
            .WithMany(x => x.TransferWindows)
            .HasForeignKey(x => x.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
