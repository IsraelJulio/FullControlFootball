using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Enums;

namespace FullControlFootball.Domain.Entities;

public sealed class Competition : ActivatableEntity
{
    private Competition() { }

    public Competition(string name, Guid? countryId, CompetitionType competitionType, int? tier, bool isDomestic, bool isContinental, string? externalSource, string? externalId)
    {
        Name = name;
        CountryId = countryId;
        CompetitionType = competitionType;
        Tier = tier;
        IsDomestic = isDomestic;
        IsContinental = isContinental;
        ExternalSource = externalSource;
        ExternalId = externalId;
    }

    public string Name { get; private set; } = null!;
    public Guid? CountryId { get; private set; }
    public CompetitionType CompetitionType { get; private set; }
    public int? Tier { get; private set; }
    public bool IsDomestic { get; private set; }
    public bool IsContinental { get; private set; }
    public string? ExternalSource { get; private set; }
    public string? ExternalId { get; private set; }

    public Country? Country { get; private set; }
}
