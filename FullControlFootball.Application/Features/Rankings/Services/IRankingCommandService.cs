using FullControlFootball.Application.Features.Rankings.Contracts;

namespace FullControlFootball.Application.Features.Rankings.Services;

public interface IRankingCommandService
{
    Task<IReadOnlyCollection<CompetitionTopScorerResponse>> CreateTopScorersAsync(Guid userId, CreateCompetitionTopScorersRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CompetitionTopAssistResponse>> CreateTopAssistsAsync(Guid userId, CreateCompetitionTopAssistsRequest request, CancellationToken cancellationToken);
}
