using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Enums;

namespace FullControlFootball.Domain.Entities;

public sealed class CompetitionStandingRow : AuditableEntity
{
    private CompetitionStandingRow() { }

    public CompetitionStandingRow(Guid competitionStandingId, Guid? saveClubId, Guid? clubId, string clubNameSnapshot, int position, int played, int wins, int draws, int losses, int goalsFor, int goalsAgainst, int goalDifference, int points, FinalStandingStatus? finalStatus)
    {
        CompetitionStandingId = competitionStandingId;
        SaveClubId = saveClubId;
        ClubId = clubId;
        ClubNameSnapshot = clubNameSnapshot;
        Position = position;
        Played = played;
        Wins = wins;
        Draws = draws;
        Losses = losses;
        GoalsFor = goalsFor;
        GoalsAgainst = goalsAgainst;
        GoalDifference = goalDifference;
        Points = points;
        FinalStatus = finalStatus;
    }

    public Guid CompetitionStandingId { get; private set; }
    public Guid? SaveClubId { get; private set; }
    public Guid? ClubId { get; private set; }
    public string ClubNameSnapshot { get; private set; } = null!;
    public int Position { get; private set; }
    public int Played { get; private set; }
    public int Wins { get; private set; }
    public int Draws { get; private set; }
    public int Losses { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }
    public int GoalDifference { get; private set; }
    public int Points { get; private set; }
    public FinalStandingStatus? FinalStatus { get; private set; }

    public CompetitionStanding CompetitionStanding { get; private set; } = null!;
    public SaveClub? SaveClub { get; private set; }
    public Club? Club { get; private set; }
}
