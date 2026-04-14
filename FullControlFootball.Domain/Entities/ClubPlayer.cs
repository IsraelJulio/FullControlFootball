using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class ClubPlayer : ActivatableEntity
{
    private ClubPlayer() { }

    public ClubPlayer(Guid clubId, Guid playerId, int? shirtNumber, string? squadRole, DateTime importedAtUtc)
    {
        ClubId = clubId;
        PlayerId = playerId;
        ShirtNumber = shirtNumber;
        SquadRole = squadRole;
        ImportedAtUtc = importedAtUtc;
    }

    public Guid ClubId { get; private set; }
    public Guid PlayerId { get; private set; }
    public int? ShirtNumber { get; private set; }
    public string? SquadRole { get; private set; }
    public DateTime ImportedAtUtc { get; private set; }

    public Club Club { get; private set; } = null!;
    public Player Player { get; private set; } = null!;
}
