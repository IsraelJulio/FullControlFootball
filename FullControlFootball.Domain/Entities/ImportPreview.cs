using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public class ImportPreview : BaseEntity
{
    public Guid ImportImageId { get; set; }
    public string RawExtractionJson { get; set; } = null!;
    public string NormalizedPreviewJson { get; set; } = null!;
    public string? UserAdjustedJson { get; set; }
    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedAtUtc { get; set; }

    public ImportImage ImportImage { get; set; } = null!;
}