using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token).HasMaxLength(FieldLengths.Token).IsRequired();
        builder.Property(x => x.CreatedByIp).HasMaxLength(FieldLengths.IpAddress);
        builder.Property(x => x.RevokedByIp).HasMaxLength(FieldLengths.IpAddress);
        builder.Property(x => x.ReplacedByToken).HasMaxLength(FieldLengths.Token);

        builder.HasIndex(x => x.Token).IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
