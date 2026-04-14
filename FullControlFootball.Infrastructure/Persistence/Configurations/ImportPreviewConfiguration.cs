using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public class ImportPreviewConfiguration : IEntityTypeConfiguration<ImportPreview>
{
    public void Configure(EntityTypeBuilder<ImportPreview> builder)
    {
        builder.ToTable("ImportPreviews");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RawExtractionJson)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(x => x.NormalizedPreviewJson)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(x => x.UserAdjustedJson)
            .HasColumnType("jsonb");

        builder.HasOne(x => x.ImportImage)
            .WithMany(x => x.Previews)
            .HasForeignKey(x => x.ImportImageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}