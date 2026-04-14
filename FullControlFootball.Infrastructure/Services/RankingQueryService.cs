using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Features.Rankings.Contracts;
using FullControlFootball.Application.Features.Rankings.Services;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Infrastructure.Services;

public sealed class RankingQueryService : IRankingQueryService
{
    private readonly IAppDbContext _dbContext;

    public RankingQueryService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<CompetitionTopScorerResponse>> GetTopScorersAsync(Guid userId, Guid seasonCompetitionId, CancellationToken cancellationToken)
    {
        var hasAccess = await _dbContext.SeasonCompetitions
            .Include(x => x.CareerSave)
            .AnyAsync(x => x.Id == seasonCompetitionId && x.CareerSave.UserId == userId, cancellationToken);

        if (!hasAccess)
        {
            throw new UnauthorizedAccessException("Season competition not found for the current user.");
        }

        return await _dbContext.CompetitionTopScorers
            .Where(x => x.SeasonCompetitionId == seasonCompetitionId)
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
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<CompetitionTopAssistResponse>> GetTopAssistsAsync(Guid userId, Guid seasonCompetitionId, CancellationToken cancellationToken)
    {
        var hasAccess = await _dbContext.SeasonCompetitions
            .Include(x => x.CareerSave)
            .AnyAsync(x => x.Id == seasonCompetitionId && x.CareerSave.UserId == userId, cancellationToken);

        if (!hasAccess)
        {
            throw new UnauthorizedAccessException("Season competition not found for the current user.");
        }

        return await _dbContext.CompetitionTopAssists
            .Where(x => x.SeasonCompetitionId == seasonCompetitionId)
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
            .ToListAsync(cancellationToken);
    }
}
