using FullControlFootball.Application.Common.Models;

namespace FullControlFootball.UnitTests.Application;

public sealed class PagedResultTests
{
    [Fact]
    public void Should_Calculate_TotalPages()
    {
        var result = new PagedResult<int>([1,2,3], 1, 2, 5);

        Assert.Equal(3, result.TotalPages);
    }
}
