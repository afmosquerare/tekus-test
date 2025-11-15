namespace Domain.Aggregates.Providers.ValueObjects;

public record Country
{
    public string Name { get; init; }
    public string Code { get; init; }

    private Country(string name, string code)
    {
        Name = name;
        Code = code;
    }
    
    protected Country() { }
    public static Country? Create(string name, string code)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(code))
            return null;

        return new Country(name, code);
    }
}