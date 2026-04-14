using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class SaveClub : AuditableEntity
{
    private SaveClub() { }

    public SaveClub(Guid careerSaveId, Guid clubId, string clubNameSnapshot, bool isUserClub)
    {
        CareerSaveId = careerSaveId;
        ClubId = clubId;
        ClubNameSnapshot = clubNameSnapshot;
        IsUserClub = isUserClub;
    }

    public Guid CareerSaveId { get; private set; }
    public Guid ClubId { get; private set; }
    public string ClubNameSnapshot { get; private set; } = null!;
    public bool IsUserClub { get; private set; }

    public CareerSave CareerSave { get; private set; } = null!;
    public Club Club { get; private set; } = null!;
}
