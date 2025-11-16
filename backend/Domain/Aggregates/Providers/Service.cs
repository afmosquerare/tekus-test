using System.Text.RegularExpressions;
using Domain.Aggregates.Providers.ValueObjects;
using Domain.Primitives;

namespace Domain.Aggregates.Providers;

public partial class Service : Entity<int>
{
    public Guid ProviderId { get; private set; }
    public string Name { get; private set; }
    public decimal HourlyRate { get; private set; }

    public IReadOnlyList<Country> Countries => _countries.AsReadOnly();

    private readonly List<Country> _countries = new();
    internal Service(Guid providerId, string name, decimal hourlyRate, List<Country> countries)
    {
        ProviderId = providerId;
        Name = name;
        HourlyRate = hourlyRate;
        _countries = countries ?? new List<Country>();
    }
    internal Service(Guid providerId, string name, decimal hourlyRate)
    {
        Id = 0;
        ProviderId = providerId;
        Name = name;
        HourlyRate = hourlyRate;
    }
    internal void AddCountry(Country country)
    {
        if (!_countries.Any(c => c.Equals(country)))
            _countries.Add(country);
    }

    private Service() { }

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