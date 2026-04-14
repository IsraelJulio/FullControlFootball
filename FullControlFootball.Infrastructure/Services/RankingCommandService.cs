using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Features.Rankings.Contracts;
using FullControlFootball.Application.Features.Rankings.Services;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Infrastructure.Services;

public sealed class RankingCommandService : IRankingCommandService
{
    private readonly IAppDbContext _dbContext;

    public RankingCommandService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<CompetitionTopScorerResponse>> CreateTopScorersAsync(Guid userId, CreateCompetitionTopScorersRequest request, CancellationToken cancellationToken)
    {
        var seasonCompetition = await LoadAuthorizedSeasonCompetitionAsync(userId, request.SeasonCompetitionId, cancellationToken);
        ValidateRankingRows(request.Rows.Select(x => (x.Position, x.SavePlayerId, x.PlayerId, x.SaveClubId)).ToList(), "top scorers");

        await ValidateSavePlayersAsync(seasonCompetition.CareerSaveId, request.Rows.Where(x => x.SavePlayerId.HasValue).Select(x => x.SavePlayerId!.Value).Distinct().ToList(), cancellationToken);
        await ValidateSaveClubsAsync(seasonCompetition.CareerSaveId, request.Rows.Where(x => x.SaveClubId.HasValue).Select(x => x.SaveClubId!.Value).Distinct().ToList(), cancellationToken);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (request.IsFinal)
            {
                var previousFinalRows = await _dbContext.CompetitionTopScorers
                    .Where(x => x.SeasonCompetitionId == request.SeasonCompetitionId && x.IsFinal)
                    .ToListAsync(cancellationToken);

                if (previousFinalRows.Count > 0)
                {
                    _dbContext.CompetitionTopScorers.RemoveRange(previousFinalRows);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            var snapshotUtc = DateTime.SpecifyKind(request.SnapshotDateUtc, DateTimeKind.Utc);

            var entities = request.Rows.Select(row => new CompetitionTopScorer(
                request.SeasonCompetitionId,
                row.SavePlayerId,
                row.PlayerId,
                row.SaveClubId,
                row.ClubId,
                row.PlayerNameSnapshot.Trim(),
                row.ClubNameSnapshot?.Trim(),
                row.Position,
                row.Goals,
                snapshotUtc,
                request.IsFinal)).ToList();

            _dbContext.CompetitionTopScorers.AddRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return entities
                .OrderBy(x => x.Position)
                .Select(x => new CompetitionTopScorerResponse(
                    x.Id,
                    x.SeasonCompetitionId,
                    x.PlayerNameSnapshot,
                    x.ClubNameSnapshot,
                    x.Position,
                    x.Goals,
                    x.SnapshotDateUtc,
                    x.IsFinal))
                .ToList();
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<IReadOnlyCollection<CompetitionTopAssistResponse>> CreateTopAssistsAsync(Guid userId, CreateCompetitionTopAssistsRequest request, CancellationToken cancellationToken)
    {
        var seasonCompetition = await LoadAuthorizedSeasonCompetitionAsync(userId, request.SeasonCompetitionId, cancellationToken);
        ValidateRankingRows(request.Rows.Select(x => (x.Position, x.SavePlayerId, x.PlayerId, x.SaveClubId)).ToList(), "top assists");

        await ValidateSavePlayersAsync(seasonCompetition.CareerSaveId, request.Rows.Where(x => x.SavePlayerId.HasValue).Select(x => x.SavePlayerId!.Value).Distinct().ToList(), cancellationToken);
        await ValidateSaveClubsAsync(seasonCompetition.CareerSaveId, request.Rows.Where(x => x.SaveClubId.HasValue).Select(x => x.SaveClubId!.Value).Distinct().ToList(), cancellationToken);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (request.IsFinal)
            {
                var previousFinalRows = await _dbContext.CompetitionTopAssists
                    .Where(x => x.SeasonCompetitionId == request.SeasonCompetitionId && x.IsFinal)
                    .ToListAsync(cancellationToken);

                if (previousFinalRows.Count > 0)
                {
                    _dbContext.CompetitionTopAssists.RemoveRange(previousFinalRows);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            var snapshotUtc = DateTime.SpecifyKind(request.SnapshotDateUtc, DateTimeKind.Utc);

            var entities = request.Rows.Select(row => new CompetitionTopAssist(
                request.SeasonCompetitionId,
                row.SavePlayerId,
                row.PlayerId,
                row.SaveClubId,
                row.ClubId,
                row.PlayerNameSnapshot.Trim(),
                row.ClubNameSnapshot?.Trim(),
                row.Position,
                row.Assists,
                snapshotUtc,
                request.IsFinal)).ToList();

            _dbContext.CompetitionTopAssists.AddRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return entities
                .OrderBy(x => x.Position)
                .Select(x => new CompetitionTopAssistResponse(
                    x.Id,
                    x.SeasonCompetitionId,
                    x.PlayerNameSnapshot,
                    x.ClubNameSnapshot,
                    x.Position,
                    x.Assists,
                    x.SnapshotDateUtc,
                    x.IsFinal))
                .ToList();
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private async Task<SeasonCompetition> LoadAuthorizedSeasonCompetitionAsync(Guid userId, Guid seasonCompetitionId, CancellationToken cancellationToken)
    {
        var seasonCompetition = await _dbContext.SeasonCompetitions
            .Include(x => x.CareerSave)
            .SingleOrDefaultAsync(x => x.Id == seasonCompetitionId, cancellationToken)
            ?? throw new InvalidOperationException("Season competition was not found.");

        if (seasonCompetition.CareerSave.UserId != userId)
        {
            throw new UnauthorizedAccessException("Season competition does not belong to the current user.");
        }

        return seasonCompetition;
    }

    private async Task ValidateSavePlayersAsync(Guid careerSaveId, IReadOnlyCollection<Guid> savePlayerIds, CancellationToken cancellationToken)
    {
        if (savePlayerIds.Count == 0)
        {
            return;
        }

        var validIds = await _dbContext.SavePlayers
            .Where(x => x.CareerSaveId == careerSaveId && savePlayerIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        if (savePlayerIds.Except(validIds).Any())
        {
            throw new InvalidOperationException("One or more ranking rows reference a SavePlayer outside the current career save.");
        }
    }

    private async Task ValidateSaveClubsAsync(Guid careerSaveId, IReadOnlyCollection<Guid> saveClubIds, CancellationToken cancellationToken)
    {
        if (saveClubIds.Count == 0)
        {
            return;
        }

        var validIds = await _dbContext.SaveClubs
            .Where(x => x.CareerSaveId == careerSaveId && saveClubIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        if (saveClubIds.Except(validIds).Any())
        {
            throw new InvalidOperationException("One or more ranking rows reference a SaveClub outside the current career save.");
        }
    }

    private static void ValidateRankingRows(IReadOnlyCollection<(int Position, Guid? SavePlayerId, Guid? PlayerId, Guid? SaveClubId)> rows, string rankingName)
    {
        var duplicatePositions = rows.GroupBy(x => x.Position).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
        if (duplicatePositions.Count > 0)
        {
            throw new InvalidOperationException($"{rankingName} positions must be unique.");
        }

        var duplicateSavePlayers = rows
            .Where(x => x.SavePlayerId.HasValue)
            .GroupBy(x => x.SavePlayerId)
            .Where(g => g.Count() > 1)
            .ToList();

        if (duplicateSavePlayers.Count > 0)
        {
            throw new InvalidOperationException($"A SavePlayer cannot appear more than once in the same {rankingName} snapshot.");
        }

        var duplicatePlayers = rows
            .Where(x => x.PlayerId.HasValue)
            .GroupBy(x => x.PlayerId)
            .Where(g => g.Count() > 1)
            .ToList();

        if (duplicatePlayers.Count > 0)
        {
            throw new InvalidOperationException($"A Player cannot appear more than once in the same {rankingName} snapshot.");
        }
    }
}
