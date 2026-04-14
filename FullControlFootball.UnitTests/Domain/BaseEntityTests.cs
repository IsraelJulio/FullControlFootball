using FullControlFootball.Domain.Entities;

namespace FullControlFootball.UnitTests.Domain;

public sealed class BaseEntityTests
{
    [Fact]
    public void User_Should_Generate_Id_On_Creation()
    {
        var user = new User("Israel", "israel@example.com", "hash", null, null);

        Assert.NotEqual(Guid.Empty, user.Id);
    }
}
