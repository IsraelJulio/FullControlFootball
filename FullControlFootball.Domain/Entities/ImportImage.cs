using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Enums;

namespace FullControlFootball.Domain.Entities;

public class ImportImage : BaseEntity
{
    public Guid CareerSaveId { get; set; }
    public Guid UploadedByUserId { get; set; }
    public string FileUrl { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string MimeType { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string? ImageHash { get; set; }
    public ImportType ImportType { get; set; }
    public ImportProcessingStatus ProcessingStatus { get; set; } = ImportProcessingStatus.Uploaded;
    public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAtUtc { get; set; }

    public CareerSave CareerSave { get; set; } = null!;
    public User UploadedByUser { get; set; } = null!;
    public ICollection<ImportPreview> Previews { get; set; } = new List<ImportPreview>();
    public ICollection<ImportError> Errors { get; set; } = new List<ImportError>();
}