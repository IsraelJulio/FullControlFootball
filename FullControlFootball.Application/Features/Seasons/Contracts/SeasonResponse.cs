namespace FullControlFootball.Application.Features.Seasons.Contracts;

public sealed record SeasonResponse(Guid Id, Guid CareerSaveId, int Number, string Label, DateOnly? StartedAt, DateOnly? EndedAt, bool IsFinished, DateTime CreatedAtUtc);
