using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class TransferWindow : AuditableEntity
{
    private readonly List<TransferTransaction> _transactions = [];

    private TransferWindow() { }

    public TransferWindow(Guid careerSaveId, Guid seasonId, string name, DateOnly startDate, DateOnly endDate, bool isOpen)
    {
        CareerSaveId = careerSaveId;
        SeasonId = seasonId;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        IsOpen = isOpen;
    }

    public Guid CareerSaveId { get; private set; }
    public Guid SeasonId { get; private set; }
    public string Name { get; private set; } = null!;
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsOpen { get; private set; }

    public CareerSave CareerSave { get; private set; } = null!;
    public Season Season { get; private set; } = null!;
    public IReadOnlyCollection<TransferTransaction> Transactions => _transactions;
}
