namespace FullControlFootball.Application.Features.Transfers.Contracts;

using FullControlFootball.Domain.Enums; public sealed record CreateTransferTransactionRequest(Guid CareerSaveId, Guid SeasonId, Guid? TransferWindowId, Guid? SavePlayerId, Guid? PlayerId, Guid? FromSaveClubId, Guid? ToSaveClubId, string? FromClubNameSnapshot, string? ToClubNameSnapshot, string PlayerNameSnapshot, TransferType TransferType, decimal? Amount, string? Currency, DateOnly TransactionDate, string? Notes);
