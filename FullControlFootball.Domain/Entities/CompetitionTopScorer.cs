using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class CompetitionTopScorer : AuditableEntity
{
    private CompetitionTopScorer() { }

    public CompetitionTopScorer(Guid seasonCompetitionId, Guid? savePlayerId, Guid? playerId, Guid? saveClubId, Guid? clubId, string playerNameSnapshot, string? clubNameSnapshot, int position, int goals, DateTime snapshotDateUtc, bool isFinal)
    {
        SeasonCompetitionId = seasonCompetitionId;
        SavePlayerId = savePlayerId;
        PlayerId = playerId;
        SaveClubId = saveClubId;
        ClubId = clubId;
        PlayerNameSnapshot = playerNameSnapshot;
        ClubNameSnapshot = clubNameSnapshot;
        Position = position;
        Goals = goals;
        SnapshotDateUtc = snapshotDateUtc;
        IsFinal = isFinal;
    }

    public Guid SeasonCompetitionId { get; private set; }
    public Guid? SavePlayerId { get; private set; }
    public Guid? PlayerId { get; private set; }
    public Guid? SaveClubId { get; private set; }
    public Guid? ClubId { get; private set; }
    public string PlayerNameSnapshot { get; private set; } = null!;
    public string? ClubNameSnapshot { get; private set; }
    public int Position { get; private set; }
    public int Goals { get; private set; }
    public DateTime SnapshotDateUtc { get; private set; }
    public bool IsFinal { get; private set; }

    public SeasonCompetition SeasonCompetition { get; private set; } = null!;
    public SavePlayer? SavePlayer { get; private set; }
    public Player? Player { get; private set; }
    public SaveClub? SaveClub { get; private set; }
    public Club? Club { get; private set; }
}
