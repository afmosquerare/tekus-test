namespace Application.Commands.Providers.Dtos;

public record ServicesDto(
    string Name,
    decimal HourlyRate,
    List<CountryDto> Countries 
);