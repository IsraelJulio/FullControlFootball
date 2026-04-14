using FullControlFootball.Application.Features.Rankings.Contracts;

namespace FullControlFootball.Application.Features.Rankings.Services;

public interface IRankingQueryService
{
    Task<IReadOnlyCollection<CompetitionTopScorerResponse>> GetTopScorersAsync(Guid userId, Guid seasonCompetitionId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CompetitionTopAssistResponse>> GetTopAssistsAsync(Guid userId, Guid seasonCompetitionId, CancellationToken cancellationToken);
}
