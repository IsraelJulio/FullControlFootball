using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class Club : ActivatableEntity
{
    private Club() { }

    public Club(string name, string? shortName, Guid? countryId, Guid? defaultCompetitionId, string? logoUrl, string? primaryColor, string? secondaryColor, string? externalSource, string? externalId)
    {
        Name = name;
        ShortName = shortName;
        CountryId = countryId;
        DefaultCompetitionId = defaultCompetitionId;
        LogoUrl = logoUrl;
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        ExternalSource = externalSource;
        ExternalId = externalId;
    }

    public string Name { get; private set; } = null!;
    public string? ShortName { get; private set; }
    public Guid? CountryId { get; private set; }
    public Guid? DefaultCompetitionId { get; private set; }
    public string? LogoUrl { get; private set; }
    public string? PrimaryColor { get; private set; }
    public string? SecondaryColor { get; private set; }
    public string? ExternalSource { get; private set; }
    public string? ExternalId { get; private set; }

    public Country? Country { get; private set; }
    public Competition? DefaultCompetition { get; private set; }
}
