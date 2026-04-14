using FullControlFootball.Application.Features.CareerSaves.Contracts;

namespace FullControlFootball.Application.Features.CareerSaves.Services;

public interface ICareerSaveService
{
    Task<CareerSaveResponse> CreateAsync(Guid userId, CreateCareerSaveRequest request, CancellationToken cancellationToken);
}
