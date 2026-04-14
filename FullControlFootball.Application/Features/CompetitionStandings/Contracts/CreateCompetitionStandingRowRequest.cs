namespace FullControlFootball.Application.Features.CompetitionStandings.Contracts;

using FullControlFootball.Domain.Enums; public sealed record CreateCompetitionStandingRowRequest(Guid? SaveClubId, Guid? ClubId, string ClubNameSnapshot, int Position, int Played, int Wins, int Draws, int Losses, int GoalsFor, int GoalsAgainst, int GoalDifference, int Points, FinalStandingStatus? FinalStatus);
