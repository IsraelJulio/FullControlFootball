using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class CareerSave : ActivatableEntity
{
    private readonly List<Season> _seasons = [];
    private readonly List<SaveClub> _saveClubs = [];
    private readonly List<SavePlayer> _savePlayers = [];
    private readonly List<SeasonCompetition> _seasonCompetitions = [];
    private readonly List<TransferWindow> _transferWindows = [];
    private readonly List<TransferTransaction> _transferTransactions = [];

    private CareerSave() { }

    public CareerSave(Guid userId, Guid? mainClubId, string name, string gameEdition, int currentSeasonNumber, string? description)
    {
        UserId = userId;
        MainClubId = mainClubId;
        Name = name;
        GameEdition = gameEdition;
        CurrentSeasonNumber = currentSeasonNumber;
        Description = description;
    }

    public Guid UserId { get; private set; }
    public Guid? MainClubId { get; private set; }
    public string Name { get; private set; } = null!;
    public string GameEdition { get; private set; } = null!;
    public int CurrentSeasonNumber { get; private set; }
    public string? Description { get; private set; }

    public User User { get; private set; } = null!;
    public Club? MainClub { get; private set; }

    public IReadOnlyCollection<Season> Seasons => _seasons;
    public IReadOnlyCollection<SaveClub> SaveClubs => _saveClubs;
    public IReadOnlyCollection<SavePlayer> SavePlayers => _savePlayers;
    public IReadOnlyCollection<SeasonCompetition> SeasonCompetitions => _seasonCompetitions;
    public IReadOnlyCollection<TransferWindow> TransferWindows => _transferWindows;
    public IReadOnlyCollection<TransferTransaction> TransferTransactions => _transferTransactions;
}
