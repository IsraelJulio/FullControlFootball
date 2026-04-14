using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public class ImportError : BaseEntity
{
    public Guid ImportImageId { get; set; }
    public string ErrorCode { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? TechnicalDetails { get; set; }

    public ImportImage ImportImage { get; set; } = null!;
}