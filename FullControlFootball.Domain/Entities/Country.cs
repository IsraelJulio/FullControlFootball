using FullControlFootball.Domain.Common;

namespace FullControlFootball.Domain.Entities;

public sealed class Country : ActivatableEntity
{
    private Country() { }

    public Country(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;
}
