using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Enums;

namespace FullControlFootball.Domain.Entities;

public sealed class SeasonCompetition : AuditableEntity
{
    private readonly List<CompetitionStanding> _standings = [];
    private readonly List<CompetitionTopScorer> _topScorers = [];
    private readonly List<CompetitionTopAssist> _topAssists = [];

    private SeasonCompetition() { }

    public SeasonCompetition(Guid careerSaveId, Guid seasonId, Guid competitionId, Guid? countryId, string nameSnapshot, CompetitionType competitionType, bool isUserParticipating, DateOnly? startedAt, DateOnly? endedAt)
    {
        CareerSaveId = careerSaveId;
        SeasonId = seasonId;
        CompetitionId = competitionId;
        CountryId = countryId;
        NameSnapshot = nameSnapshot;
        CompetitionType = competitionType;
        IsUserParticipating = isUserParticipating;
        StartedAt = startedAt;
        EndedAt = endedAt;
    }

    public Guid CareerSaveId { get; private set; }
    public Guid SeasonId { get; private set; }
    public Guid CompetitionId { get; private set; }
    public Guid? CountryId { get; private set; }
    public string NameSnapshot { get; private set; } = null!;
    public CompetitionType CompetitionType { get; private set; }
    public bool IsUserParticipating { get; private set; }
    public DateOnly? StartedAt { get; private set; }
    public DateOnly? EndedAt { get; private set; }

    public CareerSave CareerSave { get; private set; } = null!;
    public Season Season { get; private set; } = null!;
    public Competition Competition { get; private set; } = null!;
    public Country? Country { get; private set; }

    public IReadOnlyCollection<CompetitionStanding> Standings => _standings;
    public IReadOnlyCollection<CompetitionTopScorer> TopScorers => _topScorers;
    public IReadOnlyCollection<CompetitionTopAssist> TopAssists => _topAssists;
}
