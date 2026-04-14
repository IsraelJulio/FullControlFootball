using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullControlFootball.Infrastructure.Persistence.Configurations;

public sealed class TransferTransactionConfiguration : IEntityTypeConfiguration<TransferTransaction>
{
    public void Configure(EntityTypeBuilder<TransferTransaction> builder)
    {
        builder.ToTable("transfer_transactions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlayerNameSnapshot).HasMaxLength(FieldLengths.SnapshotName).IsRequired();
        builder.Property(x => x.FromClubNameSnapshot).HasMaxLength(FieldLengths.SnapshotName);
        builder.Property(x => x.ToClubNameSnapshot).HasMaxLength(FieldLengths.SnapshotName);
        builder.Property(x => x.Currency).HasMaxLength(FieldLengths.Currency);
        builder.Property(x => x.Notes).HasMaxLength(FieldLengths.Notes);
        builder.Property(x => x.Amount).HasPrecision(18, 2);

        builder.HasIndex(x => x.CareerSaveId);
        builder.HasIndex(x => x.SeasonId);
        builder.HasIndex(x => x.TransferWindowId);
        builder.HasIndex(x => x.SavePlayerId);
        builder.HasIndex(x => x.PlayerId);
        builder.HasIndex(x => x.TransactionDate);

        builder.HasOne(x => x.CareerSave).WithMany(x => x.TransferTransactions).HasForeignKey(x => x.CareerSaveId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Season).WithMany(x => x.TransferTransactions).HasForeignKey(x => x.SeasonId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.TransferWindow).WithMany(x => x.Transactions).HasForeignKey(x => x.TransferWindowId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.SavePlayer).WithMany().HasForeignKey(x => x.SavePlayerId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Player).WithMany().HasForeignKey(x => x.PlayerId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.FromSaveClub).WithMany().HasForeignKey(x => x.FromSaveClubId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.ToSaveClub).WithMany().HasForeignKey(x => x.ToSaveClubId).OnDelete(DeleteBehavior.SetNull);
    }
}
