namespace FullControlFootball.Application.Features.CompetitionStandings.Contracts;

public sealed record CompetitionStandingResponse(Guid Id, Guid SeasonCompetitionId, DateTime SnapshotDateUtc, bool IsFinal);
