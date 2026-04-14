using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Features.Transfers.Contracts;
using FullControlFootball.Application.Features.Transfers.Services;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Infrastructure.Services;

public sealed class TransferService : ITransferService
{
    private readonly IAppDbContext _dbContext;

    public TransferService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TransferTransactionResponse> CreateAsync(Guid userId, CreateTransferTransactionRequest request, CancellationToken cancellationToken)
    {
        var saveBelongsToUser = await _dbContext.CareerSaves.AnyAsync(x => x.Id == request.CareerSaveId && x.UserId == userId, cancellationToken);
        if (!saveBelongsToUser)
        {
            throw new UnauthorizedAccessException("Career save not found for the current user.");
        }

        var entity = new TransferTransaction(
            request.CareerSaveId,
            request.SeasonId,
            request.TransferWindowId,
            request.SavePlayerId,
            request.PlayerId,
            request.FromSaveClubId,
            request.ToSaveClubId,
            request.FromClubNameSnapshot?.Trim(),
            request.ToClubNameSnapshot?.Trim(),
            request.PlayerNameSnapshot.Trim(),
            request.TransferType,
            request.Amount,
            request.Currency?.Trim(),
            request.TransactionDate,
            request.Notes?.Trim());

        _dbContext.TransferTransactions.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new TransferTransactionResponse(entity.Id, entity.CareerSaveId, entity.SeasonId, entity.PlayerNameSnapshot, entity.TransferType, entity.Amount, entity.Currency, entity.TransactionDate);
    }
}
