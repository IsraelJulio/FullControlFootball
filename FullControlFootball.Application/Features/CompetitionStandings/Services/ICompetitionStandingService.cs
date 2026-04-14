using FullControlFootball.Application.Features.CompetitionStandings.Contracts;

namespace FullControlFootball.Application.Features.CompetitionStandings.Services;

public interface ICompetitionStandingService
{
    Task<CompetitionStandingResponse> CreateAsync(Guid userId, CreateCompetitionStandingRequest request, CancellationToken cancellationToken);
}
