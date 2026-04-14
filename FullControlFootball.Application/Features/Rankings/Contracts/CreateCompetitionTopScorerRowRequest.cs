namespace FullControlFootball.Application.Features.Rankings.Contracts;

public sealed record CreateCompetitionTopScorerRowRequest(
    Guid? SavePlayerId,
    Guid? PlayerId,
    Guid? SaveClubId,
    Guid? ClubId,
    string PlayerNameSnapshot,
    string? ClubNameSnapshot,
    int Position,
    int Goals);
