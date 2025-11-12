using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public partial record Email
{
    public string Value { get; set; }
    private const string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    private Email(string email) => Value = email;

    public static Email? Create( string email)
    {
        if( string.IsNullOrEmpty( email) ||  !EmailRegex().IsMatch( email))
        {
            return null;
        }
        return new Email( email );
    }

    [GeneratedRegex( pattern: Pattern)]
    private static partial Regex EmailRegex();
}