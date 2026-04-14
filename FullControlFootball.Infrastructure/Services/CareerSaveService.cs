using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Features.CareerSaves.Contracts;
using FullControlFootball.Application.Features.CareerSaves.Services;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Infrastructure.Services;

public sealed class CareerSaveService : ICareerSaveService
{
    private readonly IAppDbContext _dbContext;

    public CareerSaveService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CareerSaveResponse> CreateAsync(Guid userId, CreateCareerSaveRequest request, CancellationToken cancellationToken)
    {
        var entity = new CareerSave(userId, request.MainClubId, request.Name.Trim(), request.GameEdition.Trim(), request.CurrentSeasonNumber, request.Description?.Trim());

        _dbContext.CareerSaves.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CareerSaveResponse(entity.Id, entity.Name, entity.GameEdition, entity.CurrentSeasonNumber, entity.Description, entity.MainClubId, entity.IsActive, entity.CreatedAtUtc);
    }
}
