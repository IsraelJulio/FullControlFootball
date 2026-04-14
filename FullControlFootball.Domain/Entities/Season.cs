using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class Season : AuditableEntity
{
    private readonly List<SeasonCompetition> _seasonCompetitions = [];
    private readonly List<TransferWindow> _transferWindows = [];
    private readonly List<TransferTransaction> _transferTransactions = [];

    private Season() { }

    public Season(Guid careerSaveId, int number, string label, DateOnly? startedAt, DateOnly? endedAt, bool isFinished)
    {
        CareerSaveId = careerSaveId;
        Number = number;
        Label = label;
        StartedAt = startedAt;
        EndedAt = endedAt;
        IsFinished = isFinished;
    }

    public Guid CareerSaveId { get; private set; }
    public int Number { get; private set; }
    public string Label { get; private set; } = null!;
    public DateOnly? StartedAt { get; private set; }
    public DateOnly? EndedAt { get; private set; }
    public bool IsFinished { get; private set; }

    public CareerSave CareerSave { get; private set; } = null!;
    public IReadOnlyCollection<SeasonCompetition> SeasonCompetitions => _seasonCompetitions;
    public IReadOnlyCollection<TransferWindow> TransferWindows => _transferWindows;
    public IReadOnlyCollection<TransferTransaction> TransferTransactions => _transferTransactions;
}
