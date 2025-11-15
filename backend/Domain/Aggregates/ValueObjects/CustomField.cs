using System.Text.RegularExpressions;
using Domain.Primitives;

namespace Domain.Aggregates.Providers.ValueObjects;

public record CustomField
{
    public string FieldName { get; init; }
    public string FieldValue { get; init; }
    public string FieldType { get; init; }
    private CustomField(string fieldname, string fieldValue, string fieldType)
    {
        FieldName = fieldname;
        FieldValue = fieldValue;
        FieldType = fieldType;
    }

    protected CustomField() { } 

    public static CustomField? Create( string fieldName, string fieldValue, string fieldType)
    {
        if( string.IsNullOrWhiteSpace( fieldName) || string.IsNullOrWhiteSpace( fieldValue) || string.IsNullOrWhiteSpace( fieldType) ) return null;
        return new CustomField( fieldName, fieldValue, fieldType);
    }

}