namespace FullControlFootball.Application.Features.Seasons.Contracts;

public sealed record CreateSeasonRequest(Guid CareerSaveId, int Number, string Label, DateOnly? StartedAt, DateOnly? EndedAt, bool IsFinished);
