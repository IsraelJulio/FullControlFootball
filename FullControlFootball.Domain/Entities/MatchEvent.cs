using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Enums;

namespace FullControlFootball.Domain.Entities;

public class MatchEvent : BaseEntity
{
    public Guid MatchId { get; set; }
    public Guid? SavePlayerId { get; set; }
    public Guid? SaveClubId { get; set; }
    public string PlayerNameSnapshot { get; set; } = null!;
    public string ClubNameSnapshot { get; set; } = null!;
    public MatchEventType EventType { get; set; }
    public int? Minute { get; set; }
    public int? AdditionalTime { get; set; }

    public Match Match { get; set; } = null!;
    public SavePlayer? SavePlayer { get; set; }
    public SaveClub? SaveClub { get; set; }
}