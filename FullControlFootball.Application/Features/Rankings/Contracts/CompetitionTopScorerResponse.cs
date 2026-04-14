namespace FullControlFootball.Application.Features.Rankings.Contracts;

public sealed record CompetitionTopScorerResponse(Guid Id, Guid SeasonCompetitionId, string PlayerNameSnapshot, string? ClubNameSnapshot, int Position, int Goals, DateTime SnapshotDateUtc, bool IsFinal);
