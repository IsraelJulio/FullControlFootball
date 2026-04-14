namespace FullControlFootball.Application.Common.Security;

public interface ICurrentUserAccessor
{
    Guid GetRequiredUserId();
}
