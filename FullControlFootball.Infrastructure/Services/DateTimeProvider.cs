using FullControlFootball.Application.Abstractions.Time;

namespace FullControlFootball.Infrastructure.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
