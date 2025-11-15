using System.Text.RegularExpressions;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;

namespace Domain.Aggregates.Providers;

public partial class Service : Entity
{
    public string Name { get; private set; }
    public decimal HourlyRate { get; private set; }

    public IReadOnlyList<Country> Countries => _countries.AsReadOnly();

    private readonly List<Country> _countries = new();
    internal Service(string name, decimal hourlyRate, List<Country> countries)
    {
        Name = name;
        HourlyRate = hourlyRate;
        _countries = countries ?? new List<Country>();
    }
    internal void AddCountry(Country country)
    {
        if (!_countries.Any(c => c.Equals(country)))
            _countries.Add(country);
    }

    private Service(){}

    internal void RemoveCountry(string code)
    {
        _countries.RemoveAll(c => c.Code == code);
    }

    internal void UpdateService(string name, decimal hourlyRate)
    {
        Name = name;
        HourlyRate = hourlyRate;
    }



}