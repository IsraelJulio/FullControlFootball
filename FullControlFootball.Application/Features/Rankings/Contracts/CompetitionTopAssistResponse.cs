namespace FullControlFootball.Application.Features.Rankings.Contracts;

public sealed record CompetitionTopAssistResponse(Guid Id, Guid SeasonCompetitionId, string PlayerNameSnapshot, string? ClubNameSnapshot, int Position, int Assists, DateTime SnapshotDateUtc, bool IsFinal);
