using UnitConverter.Api.Conversion;

namespace UnitConverter.Tests;

public sealed class ConversionEngineTests
{
    private readonly ConversionEngine _engine = new(new UnitCatalog());

    [Fact]
    public void Converts_meter_to_foot_with_high_precision()
    {
        var actual = _engine.Convert(100m, "length", "meter", "foot");
        Assert.Equal(328.08398950131233595800524934m, actual);
    }

    [Fact]
    public void Converts_kilometer_to_meter()
    {
        Assert.Equal(1000m, _engine.Convert(1m, "length", "kilometer", "meter"));
    }

    [Fact]
    public void Converts_mile_to_kilometer()
    {
        Assert.Equal(1.609344m, _engine.Convert(1m, "length", "mile", "kilometer"));
    }

    [Fact]
    public void Converts_celsius_to_fahrenheit()
    {
        Assert.Equal(212m, _engine.Convert(100m, "temperature", "celsius", "fahrenheit"));
    }

    [Fact]
    public void Converts_fahrenheit_to_celsius()
    {
        Assert.Equal(0m, _engine.Convert(32m, "temperature", "fahrenheit", "celsius"));
    }

    [Fact]
    public void Converts_kelvin_to_celsius()
    {
        Assert.Equal(-273.15m, _engine.Convert(0m, "temperature", "kelvin", "celsius"));
    }

    [Fact]
    public void Rejects_temperature_below_absolute_zero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _engine.Convert(-274m, "temperature", "celsius", "kelvin"));
    }

    [Fact]
    public void Converts_decimal_storage_units()
    {
        Assert.Equal(1000000m, _engine.Convert(1m, "data_storage", "megabyte", "byte"));
        Assert.Equal(1048576m, _engine.Convert(1m, "data_storage", "mebibyte", "byte"));
    }

    [Fact]
    public void Converts_pressure()
    {
        Assert.Equal(101325m, _engine.Convert(1m, "pressure", "atmosphere", "pascal"));
    }

    [Fact]
    public void Converts_fuel_economy_reciprocally()
    {
        Assert.Equal(10m, _engine.Convert(10m, "fuel_economy",
            "kilometer_per_liter", "liter_per_100_kilometer"));
        Assert.Equal(10m, _engine.Convert(10m, "fuel_economy",
            "liter_per_100_kilometer", "kilometer_per_liter"));
    }

    [Fact]
    public void Unit_ids_are_case_insensitive()
    {
        Assert.Equal(1000m, _engine.Convert(1m, "LENGTH", "Kilometer", "METER"));
    }

    [Fact]
    public void Rejects_unknown_category()
    {
        Assert.Throws<UnknownCategoryException>(() =>
            _engine.Convert(1m, "fictional", "a", "b"));
    }

    [Fact]
    public void Rejects_unit_from_wrong_category()
    {
        Assert.Throws<UnknownUnitException>(() =>
            _engine.Convert(1m, "length", "meter", "kilogram"));
    }

    [Fact]
    public void Rejects_negative_fuel_economy()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _engine.Convert(-1m, "fuel_economy", "kilometer_per_liter", "mile_per_gallon_us"));
    }

    [Fact]
    public void Supports_more_than_thirty_categories()
    {
        Assert.True(new UnitCatalog().Categories.Count >= 30);
    }

    [Fact]
    public void Same_unit_conversion_returns_original_value()
    {
        Assert.Equal(123.456789m,
            _engine.Convert(123.456789m, "length", "meter", "meter"));
    }
}
