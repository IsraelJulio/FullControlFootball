using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Features.Seasons.Contracts;
using FullControlFootball.Application.Features.Seasons.Services;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Infrastructure.Services;

public sealed class SeasonService : ISeasonService
{
    private readonly IAppDbContext _dbContext;

    public SeasonService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SeasonResponse> CreateAsync(Guid userId, CreateSeasonRequest request, CancellationToken cancellationToken)
    {
        var saveBelongsToUser = await _dbContext.CareerSaves.AnyAsync(x => x.Id == request.CareerSaveId && x.UserId == userId, cancellationToken);
        if (!saveBelongsToUser)
        {
            throw new UnauthorizedAccessException("Career save not found for the current user.");
        }

        var entity = new Season(request.CareerSaveId, request.Number, request.Label.Trim(), request.StartedAt, request.EndedAt, request.IsFinished);

        _dbContext.Seasons.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SeasonResponse(entity.Id, entity.CareerSaveId, entity.Number, entity.Label, entity.StartedAt, entity.EndedAt, entity.IsFinished, entity.CreatedAtUtc);
    }
}
