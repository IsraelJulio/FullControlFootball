namespace FullControlFootball.Application.Features.Rankings.Contracts;

public sealed record CreateCompetitionTopAssistsRequest(
    Guid SeasonCompetitionId,
    DateTime SnapshotDateUtc,
    bool IsFinal,
    IReadOnlyCollection<CreateCompetitionTopAssistRowRequest> Rows);
