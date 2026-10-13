namespace UnitConverter.Api.Conversion;

/// <summary>
/// Factor converts a unit value to the category's canonical base unit:
/// baseValue = value * Factor. Offset is used only for affine categories:
/// baseValue = (value + Offset) * Factor.
/// </summary>
public sealed record UnitDefinition(
    string Id,
    string Symbol,
    string Description,
    decimal Factor,
    decimal Offset = 0m);

public sealed record UnitCategoryDefinition(
    string Id,
    string Name,
    string BaseUnit,
    IReadOnlyDictionary<string, UnitDefinition> Units);
