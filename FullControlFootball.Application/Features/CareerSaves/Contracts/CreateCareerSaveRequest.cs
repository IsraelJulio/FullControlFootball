namespace FullControlFootball.Application.Features.CareerSaves.Contracts;

public sealed record CreateCareerSaveRequest(Guid? MainClubId, string Name, string GameEdition, int CurrentSeasonNumber, string? Description);
