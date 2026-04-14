using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(FieldLengths.Name).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(FieldLengths.Email).IsRequired();
        builder.Property(x => x.PasswordHash).HasMaxLength(FieldLengths.PasswordHash);
        builder.Property(x => x.ProfileImageUrl).HasMaxLength(FieldLengths.Url);
        builder.Property(x => x.PreferredTheme).HasMaxLength(FieldLengths.Theme);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.CareerSaves)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.RefreshTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.AuthProviders)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
