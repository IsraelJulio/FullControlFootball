using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class Player : ActivatableEntity
{
    private Player() { }

    public Player(string name, string? knownAs, DateOnly? birthDate, Guid? nationalityCountryId, string primaryPosition, string? secondaryPositions, int? overallBase, int? potentialBase, string? preferredFoot, string? faceImageUrl, string? externalSource, string? externalId)
    {
        Name = name;
        KnownAs = knownAs;
        BirthDate = birthDate;
        NationalityCountryId = nationalityCountryId;
        PrimaryPosition = primaryPosition;
        SecondaryPositions = secondaryPositions;
        OverallBase = overallBase;
        PotentialBase = potentialBase;
        PreferredFoot = preferredFoot;
        FaceImageUrl = faceImageUrl;
        ExternalSource = externalSource;
        ExternalId = externalId;
    }

    public string Name { get; private set; } = null!;
    public string? KnownAs { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public Guid? NationalityCountryId { get; private set; }
    public string PrimaryPosition { get; private set; } = null!;
    public string? SecondaryPositions { get; private set; }
    public int? OverallBase { get; private set; }
    public int? PotentialBase { get; private set; }
    public string? PreferredFoot { get; private set; }
    public string? FaceImageUrl { get; private set; }
    public string? ExternalSource { get; private set; }
    public string? ExternalId { get; private set; }

    public Country? NationalityCountry { get; private set; }
}
