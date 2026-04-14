using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Enums;

namespace FullControlFootball.Domain.Entities;

public sealed class TransferTransaction : AuditableEntity
{
    private TransferTransaction() { }

    public TransferTransaction(Guid careerSaveId, Guid seasonId, Guid? transferWindowId, Guid? savePlayerId, Guid? playerId, Guid? fromSaveClubId, Guid? toSaveClubId, string? fromClubNameSnapshot, string? toClubNameSnapshot, string playerNameSnapshot, TransferType transferType, decimal? amount, string? currency, DateOnly transactionDate, string? notes)
    {
        CareerSaveId = careerSaveId;
        SeasonId = seasonId;
        TransferWindowId = transferWindowId;
        SavePlayerId = savePlayerId;
        PlayerId = playerId;
        FromSaveClubId = fromSaveClubId;
        ToSaveClubId = toSaveClubId;
        FromClubNameSnapshot = fromClubNameSnapshot;
        ToClubNameSnapshot = toClubNameSnapshot;
        PlayerNameSnapshot = playerNameSnapshot;
        TransferType = transferType;
        Amount = amount;
        Currency = currency;
        TransactionDate = transactionDate;
        Notes = notes;
    }

    public Guid CareerSaveId { get; private set; }
    public Guid SeasonId { get; private set; }
    public Guid? TransferWindowId { get; private set; }
    public Guid? SavePlayerId { get; private set; }
    public Guid? PlayerId { get; private set; }
    public Guid? FromSaveClubId { get; private set; }
    public Guid? ToSaveClubId { get; private set; }
    public string? FromClubNameSnapshot { get; private set; }
    public string? ToClubNameSnapshot { get; private set; }
    public string PlayerNameSnapshot { get; private set; } = null!;
    public TransferType TransferType { get; private set; }
    public decimal? Amount { get; private set; }
    public string? Currency { get; private set; }
    public DateOnly TransactionDate { get; private set; }
    public string? Notes { get; private set; }

    public CareerSave CareerSave { get; private set; } = null!;
    public Season Season { get; private set; } = null!;
    public TransferWindow? TransferWindow { get; private set; }
    public SavePlayer? SavePlayer { get; private set; }
    public Player? Player { get; private set; }
    public SaveClub? FromSaveClub { get; private set; }
    public SaveClub? ToSaveClub { get; private set; }
}
