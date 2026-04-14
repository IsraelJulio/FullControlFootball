namespace FullControlFootball.Application.Features.Rankings.Contracts;

public sealed record CreateCompetitionTopScorersRequest(
    Guid SeasonCompetitionId,
    DateTime SnapshotDateUtc,
    bool IsFinal,
    IReadOnlyCollection<CreateCompetitionTopScorerRowRequest> Rows);
