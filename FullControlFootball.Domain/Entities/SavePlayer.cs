using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class SavePlayer : AuditableEntity
{
    private SavePlayer() { }

    public SavePlayer(Guid careerSaveId, Guid playerId, Guid? currentClubId, string playerNameSnapshot, int? overallCurrent, int? potentialCurrent, int? ageSnapshot, string? primaryPositionSnapshot, bool isRetired)
    {
        CareerSaveId = careerSaveId;
        PlayerId = playerId;
        CurrentClubId = currentClubId;
        PlayerNameSnapshot = playerNameSnapshot;
        OverallCurrent = overallCurrent;
        PotentialCurrent = potentialCurrent;
        AgeSnapshot = ageSnapshot;
        PrimaryPositionSnapshot = primaryPositionSnapshot;
        IsRetired = isRetired;
    }

    public Guid CareerSaveId { get; private set; }
    public Guid PlayerId { get; private set; }
    public Guid? CurrentClubId { get; private set; }
    public string PlayerNameSnapshot { get; private set; } = null!;
    public int? OverallCurrent { get; private set; }
    public int? PotentialCurrent { get; private set; }
    public int? AgeSnapshot { get; private set; }
    public string? PrimaryPositionSnapshot { get; private set; }
    public bool IsRetired { get; private set; }

    public CareerSave CareerSave { get; private set; } = null!;
    public Player Player { get; private set; } = null!;
    public SaveClub? CurrentClub { get; private set; }
}
