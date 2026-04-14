namespace FullControlFootball.Domain.Enums;

public enum ImportProcessingStatus
{
    Uploaded = 1,
    Processing = 2,
    ReadyForReview = 3,
    Confirmed = 4,
    Failed = 5
}