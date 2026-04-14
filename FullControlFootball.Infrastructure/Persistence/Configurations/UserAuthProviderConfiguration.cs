using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class UserAuthProviderConfiguration : IEntityTypeConfiguration<UserAuthProvider>
{
    public void Configure(EntityTypeBuilder<UserAuthProvider> builder)
    {
        builder.ToTable("user_auth_providers");
        builder.HasKey(x => x.Id);

builder.Property(x => x.ProviderUserId).HasMaxLength(200).IsRequired();
builder.Property(x => x.ProviderEmail).HasMaxLength(320);
builder.HasIndex(x => new { x.Provider, x.ProviderUserId }).IsUnique();
builder.HasOne(x => x.User).WithMany(x => x.AuthProviders).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

    }
}
