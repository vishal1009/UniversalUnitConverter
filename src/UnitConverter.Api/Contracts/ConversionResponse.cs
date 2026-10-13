namespace UnitConverter.Api.Contracts;

public sealed record ConversionResponse(
    decimal InputValue,
    string Category,
    string FromUnit,
    string ToUnit,
    decimal Result);
