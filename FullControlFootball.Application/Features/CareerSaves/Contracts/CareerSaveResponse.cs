namespace FullControlFootball.Application.Features.CareerSaves.Contracts;

public sealed record CareerSaveResponse(Guid Id, string Name, string GameEdition, int CurrentSeasonNumber, string? Description, Guid? MainClubId, bool IsActive, DateTime CreatedAtUtc);
