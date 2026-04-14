using FullControlFootball.Application.Features.Seasons.Contracts;

namespace FullControlFootball.Application.Features.Seasons.Services;

public interface ISeasonService
{
    Task<SeasonResponse> CreateAsync(Guid userId, CreateSeasonRequest request, CancellationToken cancellationToken);
}
