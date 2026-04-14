using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Features.CompetitionStandings.Contracts;
using FullControlFootball.Application.Features.CompetitionStandings.Services;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Infrastructure.Services;

public sealed class CompetitionStandingService : ICompetitionStandingService
{
    private readonly IAppDbContext _dbContext;

    public CompetitionStandingService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CompetitionStandingResponse> CreateAsync(Guid userId, CreateCompetitionStandingRequest request, CancellationToken cancellationToken)
    {
        var seasonCompetition = await _dbContext.SeasonCompetitions
            .Include(x => x.CareerSave)
            .SingleOrDefaultAsync(x => x.Id == request.SeasonCompetitionId, cancellationToken)
            ?? throw new InvalidOperationException("Season competition was not found.");

        if (seasonCompetition.CareerSave.UserId != userId)
        {
            throw new UnauthorizedAccessException("Season competition does not belong to the current user.");
        }

        var standing = new CompetitionStanding(request.SeasonCompetitionId, DateTime.SpecifyKind(request.SnapshotDateUtc, DateTimeKind.Utc), request.IsFinal);
        _dbContext.CompetitionStandings.Add(standing);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var rows = request.Rows.Select(row => new CompetitionStandingRow(
            standing.Id,
            row.SaveClubId,
            row.ClubId,
            row.ClubNameSnapshot,
            row.Position,
            row.Played,
            row.Wins,
            row.Draws,
            row.Losses,
            row.GoalsFor,
            row.GoalsAgainst,
            row.GoalDifference,
            row.Points,
            row.FinalStatus)).ToList();

        _dbContext.CompetitionStandingRows.AddRange(rows);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CompetitionStandingResponse(standing.Id, standing.SeasonCompetitionId, standing.SnapshotDateUtc, standing.IsFinal);
    }
}
