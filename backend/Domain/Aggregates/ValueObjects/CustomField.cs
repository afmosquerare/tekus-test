using System.Text.RegularExpressions;
using Domain.Primitives;

namespace Domain.Aggregates.Providers.ValueObjects;

public record CustomField
{
    public string FieldName { get; init; }
    public string FieldValue { get; init; }
    private CustomField(string fieldname, string fieldValue)
    {
        FieldName = fieldname;
        FieldValue = fieldValue;
    }

    protected CustomField() { }

    public static CustomField? Create(string fieldName, string fieldValue)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(fieldValue)) return null;
        return new CustomField(fieldName, fieldValue);
    }


}