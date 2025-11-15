using System.Text.RegularExpressions;

namespace Domain.Aggregates.Providers.ValueObjects;

public partial record Email
{
    public string Value { get; init; }
    private const string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    protected Email(string email) => Value = email;

    public static Email? Create( string email)
    {
        if( string.IsNullOrWhiteSpace( email) ||  !EmailRegex().IsMatch( email))
        {
            return null;
        }
        return new Email( email );
    }

    [GeneratedRegex( pattern: Pattern)]
    private static partial Regex EmailRegex();

    public override string ToString() => Value;
}