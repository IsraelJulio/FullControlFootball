using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public class PlayerSeasonProgression : BaseEntity
{
    public Guid CareerSaveId { get; set; }
    public Guid SeasonId { get; set; }
    public Guid SavePlayerId { get; set; }
    public Guid? ClubAtSeasonId { get; set; }
    public int? OverallStart { get; set; }
    public int? OverallEnd { get; set; }
    public int? PotentialStart { get; set; }
    public int? PotentialEnd { get; set; }
    public int? AgeAtSeasonStart { get; set; }
    public int? AgeAtSeasonEnd { get; set; }
    public string? Notes { get; set; }

    public CareerSave CareerSave { get; set; } = null!;
    public Season Season { get; set; } = null!;
    public SavePlayer SavePlayer { get; set; } = null!;
    public SaveClub? ClubAtSeason { get; set; }
}