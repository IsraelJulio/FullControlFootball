using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public class Match : BaseEntity
{
    public Guid CareerSaveId { get; set; }
    public Guid SeasonId { get; set; }
    public Guid? SeasonCompetitionId { get; set; }
    public string? RoundLabel { get; set; }
    public DateTime? MatchDateUtc { get; set; }
    public Guid? HomeSaveClubId { get; set; }
    public Guid? AwaySaveClubId { get; set; }
    public string HomeClubNameSnapshot { get; set; } = null!;
    public string AwayClubNameSnapshot { get; set; } = null!;
    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }
    public string? Venue { get; set; }

    public CareerSave CareerSave { get; set; } = null!;
    public Season Season { get; set; } = null!;
    public SeasonCompetition? SeasonCompetition { get; set; }
    public SaveClub? HomeSaveClub { get; set; }
    public SaveClub? AwaySaveClub { get; set; }

    public ICollection<MatchEvent> Events { get; set; } = new List<MatchEvent>();
}