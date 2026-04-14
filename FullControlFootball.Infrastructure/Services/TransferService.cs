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
        var careerSave = await _dbContext.CareerSaves
            .SingleOrDefaultAsync(x => x.Id == request.CareerSaveId && x.UserId == userId, cancellationToken)
            ?? throw new UnauthorizedAccessException("Career save not found for the current user.");

        var seasonBelongsToSave = await _dbContext.Seasons
            .AnyAsync(x => x.Id == request.SeasonId && x.CareerSaveId == careerSave.Id, cancellationToken);

        if (!seasonBelongsToSave)
        {
            throw new InvalidOperationException("Season does not belong to the specified career save.");
        }

        if (request.TransferWindowId.HasValue)
        {
            var windowBelongsToSaveAndSeason = await _dbContext.TransferWindows
                .AnyAsync(x => x.Id == request.TransferWindowId.Value && x.CareerSaveId == careerSave.Id && x.SeasonId == request.SeasonId, cancellationToken);

            if (!windowBelongsToSaveAndSeason)
            {
                throw new InvalidOperationException("Transfer window does not belong to the specified season and career save.");
            }
        }

        if (request.SavePlayerId.HasValue)
        {
            var savePlayer = await _dbContext.SavePlayers
                .SingleOrDefaultAsync(x => x.Id == request.SavePlayerId.Value && x.CareerSaveId == careerSave.Id, cancellationToken)
                ?? throw new InvalidOperationException("SavePlayer does not belong to the specified career save.");

            if (!string.Equals(savePlayer.PlayerNameSnapshot.Trim(), request.PlayerNameSnapshot.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("PlayerNameSnapshot does not match the referenced SavePlayer.");
            }
        }

        if (request.FromSaveClubId.HasValue)
        {
            var fromBelongsToSave = await _dbContext.SaveClubs
                .AnyAsync(x => x.Id == request.FromSaveClubId.Value && x.CareerSaveId == careerSave.Id, cancellationToken);

            if (!fromBelongsToSave)
            {
                throw new InvalidOperationException("FromSaveClub does not belong to the specified career save.");
            }
        }

        if (request.ToSaveClubId.HasValue)
        {
            var toBelongsToSave = await _dbContext.SaveClubs
                .AnyAsync(x => x.Id == request.ToSaveClubId.Value && x.CareerSaveId == careerSave.Id, cancellationToken);

            if (!toBelongsToSave)
            {
                throw new InvalidOperationException("ToSaveClub does not belong to the specified career save.");
            }
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

        return new TransferTransactionResponse(
            entity.Id,
            entity.CareerSaveId,
            entity.SeasonId,
            entity.PlayerNameSnapshot,
            entity.TransferType,
            entity.Amount,
            entity.Currency,
            entity.TransactionDate);
    }
}
