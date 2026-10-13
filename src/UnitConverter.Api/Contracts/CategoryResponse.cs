namespace UnitConverter.Api.Contracts;

public sealed record UnitResponse(string Id, string Symbol, string Description);

public sealed record CategoryResponse(
    string Id,
    string Name,
    string BaseUnit,
    IReadOnlyList<UnitResponse> Units);
