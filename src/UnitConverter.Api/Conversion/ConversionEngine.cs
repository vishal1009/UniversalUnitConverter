namespace UnitConverter.Api.Conversion;

public sealed class ConversionEngine(UnitCatalog catalog)
{
    private const decimal AbsoluteZeroKelvin = 0m;
    private const decimal LitersPer100KmNumerator = 100m;

    public decimal Convert(decimal value, string categoryId, string fromUnitId, string toUnitId)
    {
        if (string.IsNullOrWhiteSpace(categoryId))
            throw new ArgumentException("Category is required.", nameof(categoryId));
        if (string.IsNullOrWhiteSpace(fromUnitId))
            throw new ArgumentException("Source unit is required.", nameof(fromUnitId));
        if (string.IsNullOrWhiteSpace(toUnitId))
            throw new ArgumentException("Target unit is required.", nameof(toUnitId));

        var category = catalog.GetCategory(categoryId);
        if (!category.Units.TryGetValue(fromUnitId, out var from))
            throw new UnknownUnitException(category.Id, fromUnitId);
        if (!category.Units.TryGetValue(toUnitId, out var to))
            throw new UnknownUnitException(category.Id, toUnitId);

        if (category.Id.Equals("fuel_economy", StringComparison.OrdinalIgnoreCase))
            return ConvertFuelEconomy(value, from.Id, to.Id);

        decimal baseValue;
        try
        {
            baseValue = (value + from.Offset) * from.Factor;
            if (category.Id.Equals("temperature", StringComparison.OrdinalIgnoreCase)
                && baseValue < AbsoluteZeroKelvin)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Temperature cannot be below absolute zero (0 K).");

            return baseValue / to.Factor - to.Offset;
        }
        catch (OverflowException)
        {
            throw new ArgumentOutOfRangeException(nameof(value),
                "The value is outside the supported numeric range for this conversion.");
        }
        catch (DivideByZeroException)
        {
            throw new InvalidOperationException("A unit definition has an invalid zero factor.");
        }
    }

    private static decimal ConvertFuelEconomy(decimal value, string from, string to)
    {
        if (value < 0m)
            throw new ArgumentOutOfRangeException(nameof(value),
                "Fuel economy cannot be negative.");
        if (from.Equals(to, StringComparison.OrdinalIgnoreCase))
            return value;

        // km/L is the canonical economy unit. L/100 km is reciprocal.
        decimal kmPerLiter = from switch
        {
            "kilometer_per_liter" => value,
            "mile_per_gallon_us" => value * 0.425143707430272m,
            "mile_per_gallon_imperial" => value * 0.354006189934647m,
            "liter_per_100_kilometer" => value == 0m
                ? throw new ArgumentOutOfRangeException(nameof(value),
                    "L/100 km must be greater than zero when converting to fuel economy.")
                : LitersPer100KmNumerator / value,
            _ => throw new InvalidOperationException("Unsupported fuel economy source unit.")
        };

        return to switch
        {
            "kilometer_per_liter" => kmPerLiter,
            "mile_per_gallon_us" => kmPerLiter / 0.425143707430272m,
            "mile_per_gallon_imperial" => kmPerLiter / 0.354006189934647m,
            "liter_per_100_kilometer" => kmPerLiter == 0m
                ? throw new ArgumentOutOfRangeException(nameof(value),
                    "Zero km/L cannot be represented as a finite L/100 km value.")
                : LitersPer100KmNumerator / kmPerLiter,
            _ => throw new InvalidOperationException("Unsupported fuel economy target unit.")
        };
    }
}
