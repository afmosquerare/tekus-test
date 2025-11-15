using Domain.Primitives;
using Domain.Aggregates.Providers.ValueObjects;
using ErrorOr;

namespace Domain.Aggregates.Providers;

public sealed class Provider : AggregateRoot
{
    public Provider(string name, string nit, Email email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Nit = nit;
        Email = email;
    }

    private Provider() { }
    

    public string Name { get; private set; } = string.Empty;
    public string Nit { get; private set; } = string.Empty;
    public Email Email { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    public bool IsActive { get; private set; } = true;

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;


    public void UpdateEmail(string email)
    {
        Email = Email.Create(email);
        UpdatedAt = DateTime.Now;
    }
    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.Now;
    }
    public void UpdateNit(string nit)
    {
        Nit = nit;
        UpdatedAt = DateTime.Now;
    }

    

    //customfields
    private readonly List<CustomField> _customFields = new();

   
    public IReadOnlyList<CustomField> CustomFields => _customFields.AsReadOnly();
    public void AddCustomField(string name, string value)
    {
        var field = CustomField.Create(name, value);
        if (field != null)
            _customFields.Add(field);
    }
    public void AddCustomField(CustomField customField)
    {
        var field = CustomField.Create(customField.FieldName, customField.FieldValue);
        if (field != null)
            _customFields.Add(field);
    }

    public void AddCustomsFields(List<CustomField> customsFields)
    {
        if (customsFields.Count == 0) return;
        foreach (var c in customsFields)
        {
            var field = CustomField.Create(c.FieldName, c.FieldValue);
            if (field != null)
                _customFields.Add(field);
        }
    }
    public void RemoveCustomField(CustomField field) => _customFields.Remove(field);


    //services
    private readonly List<Service> _services = new();
    public IReadOnlyCollection<Service> Services => _services.AsReadOnly();
    public void AddService(string name, decimal hourlyRate, List<Country> countries)
    {
        var service = new Service(Id, name, hourlyRate, countries);
        _services.Add(service);
    }

    public void RemoveService(Service service) => _services.Remove(service);

    public void UpdateService(Guid serviceId, string name, decimal hourlyRate)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        service?.UpdateService(name, hourlyRate);
    }
    public void AddCountryToService(Guid serviceId, Country country) => GetServiceById(serviceId)?.AddCountry(country);

    public void RemoveCountryFromService(Guid serviceId, string code) => GetServiceById(serviceId)?.RemoveCountry(code);

    private Service? GetServiceById(Guid serviceId)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        return service;
    }


}
