namespace FullControlFootball.Application.Features.CompetitionStandings.Contracts;

public sealed record CreateCompetitionStandingRequest(Guid SeasonCompetitionId, DateTime SnapshotDateUtc, bool IsFinal, IReadOnlyCollection<CreateCompetitionStandingRowRequest> Rows);
