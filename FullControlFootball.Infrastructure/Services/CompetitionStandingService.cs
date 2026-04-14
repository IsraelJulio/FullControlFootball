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

        ValidateRows(request.Rows);

        var referencedSaveClubIds = request.Rows
            .Where(x => x.SaveClubId.HasValue)
            .Select(x => x.SaveClubId!.Value)
            .Distinct()
            .ToList();

        if (referencedSaveClubIds.Count > 0)
        {
            var validSaveClubIds = await _dbContext.SaveClubs
                .Where(x => x.CareerSaveId == seasonCompetition.CareerSaveId && referencedSaveClubIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            var invalidSaveClubIds = referencedSaveClubIds.Except(validSaveClubIds).ToList();
            if (invalidSaveClubIds.Count > 0)
            {
                throw new InvalidOperationException("One or more standing rows reference a SaveClub outside the current career save.");
            }
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var standing = new CompetitionStanding(
                request.SeasonCompetitionId,
                DateTime.SpecifyKind(request.SnapshotDateUtc, DateTimeKind.Utc),
                request.IsFinal);

            _dbContext.CompetitionStandings.Add(standing);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var rows = request.Rows.Select(row => new CompetitionStandingRow(
                standing.Id,
                row.SaveClubId,
                row.ClubId,
                row.ClubNameSnapshot.Trim(),
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

            await transaction.CommitAsync(cancellationToken);

            return new CompetitionStandingResponse(standing.Id, standing.SeasonCompetitionId, standing.SnapshotDateUtc, standing.IsFinal);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static void ValidateRows(IReadOnlyCollection<CreateCompetitionStandingRowRequest> rows)
    {
        var duplicatePositions = rows.GroupBy(x => x.Position).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
        if (duplicatePositions.Count > 0)
        {
            throw new InvalidOperationException("Standing positions must be unique.");
        }

        var duplicateSaveClubs = rows
            .Where(x => x.SaveClubId.HasValue)
            .GroupBy(x => x.SaveClubId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateSaveClubs.Count > 0)
        {
            throw new InvalidOperationException("A SaveClub cannot appear more than once in the same standing snapshot.");
        }

        var duplicateClubs = rows
            .Where(x => x.ClubId.HasValue)
            .GroupBy(x => x.ClubId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateClubs.Count > 0)
        {
            throw new InvalidOperationException("A Club cannot appear more than once in the same standing snapshot.");
        }

        foreach (var row in rows)
        {
            if (row.Played != row.Wins + row.Draws + row.Losses)
            {
                throw new InvalidOperationException($"Standing row at position {row.Position} has inconsistent matches played totals.");
            }

            if (row.GoalDifference != row.GoalsFor - row.GoalsAgainst)
            {
                throw new InvalidOperationException($"Standing row at position {row.Position} has inconsistent goal difference.");
            }
        }
    }
}
