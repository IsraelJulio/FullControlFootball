namespace FullControlFootball.Application.Features.Transfers.Contracts;

using FullControlFootball.Domain.Enums; public sealed record TransferTransactionResponse(Guid Id, Guid CareerSaveId, Guid SeasonId, string PlayerNameSnapshot, TransferType TransferType, decimal? Amount, string? Currency, DateOnly TransactionDate);
