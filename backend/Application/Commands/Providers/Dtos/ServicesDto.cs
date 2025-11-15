namespace Application.Commands.Providers.Dtos;

public record ServicesDto(
    Guid Id,
    string Name,
    decimal HourlyRate,
    List<CountryDto> Countries 
);