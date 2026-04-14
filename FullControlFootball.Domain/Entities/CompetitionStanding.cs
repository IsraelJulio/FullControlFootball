using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class CompetitionStanding : AuditableEntity
{
    private readonly List<CompetitionStandingRow> _rows = [];

    private CompetitionStanding() { }

    public CompetitionStanding(Guid seasonCompetitionId, DateTime snapshotDateUtc, bool isFinal)
    {
        SeasonCompetitionId = seasonCompetitionId;
        SnapshotDateUtc = snapshotDateUtc;
        IsFinal = isFinal;
    }

    public Guid SeasonCompetitionId { get; private set; }
    public DateTime SnapshotDateUtc { get; private set; }
    public bool IsFinal { get; private set; }

    public SeasonCompetition SeasonCompetition { get; private set; } = null!;
    public IReadOnlyCollection<CompetitionStandingRow> Rows => _rows;
}
