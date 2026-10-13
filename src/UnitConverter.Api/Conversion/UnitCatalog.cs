namespace UnitConverter.Api.Conversion;

/// <summary>
/// In-memory catalogue. Linear categories convert through a canonical base unit.
/// Temperature is affine; fuel economy is handled as a reciprocal category.
/// </summary>
public sealed class UnitCatalog
{
    private readonly Dictionary<string, UnitCategoryDefinition> _categories =
        new(StringComparer.OrdinalIgnoreCase);

    public UnitCatalog()
    {
        Add("length", "Length", "meter",
            U("millimeter", "mm", "Millimeter", 0.001m),
            U("centimeter", "cm", "Centimeter", 0.01m),
            U("decimeter", "dm", "Decimeter", 0.1m),
            U("meter", "m", "Meter", 1m),
            U("kilometer", "km", "Kilometer", 1000m),
            U("micrometer", "µm", "Micrometer", 0.000001m),
            U("nanometer", "nm", "Nanometer", 0.000000001m),
            U("inch", "in", "Inch", 0.0254m),
            U("foot", "ft", "Foot", 0.3048m),
            U("yard", "yd", "Yard", 0.9144m),
            U("mile", "mi", "Mile", 1609.344m),
            U("nautical_mile", "nmi", "Nautical mile", 1852m),
            U("fathom", "ftm", "Fathom", 1.8288m),
            U("astronomical_unit", "au", "Astronomical unit", 149597870700m),
            U("light_year", "ly", "Light-year", 9460730472580800m),
            U("parsec", "pc", "Parsec", 30856775814913673m));

        Add("area", "Area", "square_meter",
            U("square_millimeter", "mm²", "Square millimeter", 0.000001m),
            U("square_centimeter", "cm²", "Square centimeter", 0.0001m),
            U("square_meter", "m²", "Square meter", 1m),
            U("square_kilometer", "km²", "Square kilometer", 1000000m),
            U("hectare", "ha", "Hectare", 10000m),
            U("acre", "ac", "Acre", 4046.8564224m),
            U("square_inch", "in²", "Square inch", 0.00064516m),
            U("square_foot", "ft²", "Square foot", 0.09290304m),
            U("square_yard", "yd²", "Square yard", 0.83612736m),
            U("square_mile", "mi²", "Square mile", 2589988.110336m));

        Add("volume", "Volume", "cubic_meter",
            U("milliliter", "mL", "Milliliter", 0.000001m),
            U("liter", "L", "Liter", 0.001m),
            U("cubic_centimeter", "cm³", "Cubic centimeter", 0.000001m),
            U("cubic_meter", "m³", "Cubic meter", 1m),
            U("cubic_kilometer", "km³", "Cubic kilometer", 1000000000m),
            U("teaspoon_us", "tsp (US)", "US teaspoon", 0.00000492892159375m),
            U("tablespoon_us", "tbsp (US)", "US tablespoon", 0.00001478676478125m),
            U("fluid_ounce_us", "fl oz (US)", "US fluid ounce", 0.0000295735295625m),
            U("cup_us", "cup (US)", "US customary cup", 0.0002365882365m),
            U("pint_us", "pt (US)", "US liquid pint", 0.000473176473m),
            U("quart_us", "qt (US)", "US liquid quart", 0.000946352946m),
            U("gallon_us", "gal (US)", "US gallon", 0.003785411784m),
            U("gallon_imperial", "gal (imp)", "Imperial gallon", 0.00454609m),
            U("cubic_inch", "in³", "Cubic inch", 0.000016387064m),
            U("cubic_foot", "ft³", "Cubic foot", 0.028316846592m),
            U("cubic_yard", "yd³", "Cubic yard", 0.764554857984m));

        Add("mass", "Mass", "kilogram",
            U("microgram", "µg", "Microgram", 0.000000001m),
            U("milligram", "mg", "Milligram", 0.000001m),
            U("gram", "g", "Gram", 0.001m),
            U("kilogram", "kg", "Kilogram", 1m),
            U("tonne", "t", "Metric tonne", 1000m),
            U("ounce", "oz", "Avoirdupois ounce", 0.028349523125m),
            U("pound", "lb", "Avoirdupois pound", 0.45359237m),
            U("stone", "st", "Stone", 6.35029318m),
            U("ton_us", "short ton", "US short ton", 907.18474m),
            U("ton_imperial", "long ton", "Imperial long ton", 1016.0469088m),
            U("atomic_mass_unit", "u", "Unified atomic mass unit", 1.66053906660e-27m));

        Add("time", "Time", "second",
            U("nanosecond", "ns", "Nanosecond", 0.000000001m),
            U("microsecond", "µs", "Microsecond", 0.000001m),
            U("millisecond", "ms", "Millisecond", 0.001m),
            U("second", "s", "Second", 1m),
            U("minute", "min", "Minute", 60m),
            U("hour", "h", "Hour", 3600m),
            U("day", "d", "Day", 86400m),
            U("week", "wk", "Week", 604800m),
            U("month_30_day", "mo", "Approximate 30-day month", 2592000m),
            U("year_365_25_day", "yr", "Julian year (365.25 days)", 31557600m));

        Add("speed", "Speed", "meter_per_second",
            U("meter_per_second", "m/s", "Meter per second", 1m),
            U("kilometer_per_hour", "km/h", "Kilometer per hour", 1m / 3.6m),
            U("mile_per_hour", "mph", "Mile per hour", 0.44704m),
            U("foot_per_second", "ft/s", "Foot per second", 0.3048m),
            U("knot", "kn", "Knot", 0.5144444444444444444444444444m));

        Add("acceleration", "Acceleration", "meter_per_second_squared",
            U("meter_per_second_squared", "m/s²", "Meter per second squared", 1m),
            U("foot_per_second_squared", "ft/s²", "Foot per second squared", 0.3048m),
            U("standard_gravity", "g₀", "Standard gravity", 9.80665m));

        Add("temperature", "Temperature", "kelvin",
            U("kelvin", "K", "Kelvin", 1m),
            U("celsius", "°C", "Degrees Celsius", 1m, 273.15m),
            U("fahrenheit", "°F", "Degrees Fahrenheit", 5m / 9m, 459.67m),
            U("rankine", "°R", "Degrees Rankine", 5m / 9m, 0m));

        Add("pressure", "Pressure", "pascal",
            U("pascal", "Pa", "Pascal", 1m),
            U("kilopascal", "kPa", "Kilopascal", 1000m),
            U("megapascal", "MPa", "Megapascal", 1000000m),
            U("bar", "bar", "Bar", 100000m),
            U("millibar", "mbar", "Millibar", 100m),
            U("atmosphere", "atm", "Standard atmosphere", 101325m),
            U("technical_atmosphere", "at", "Technical atmosphere", 98066.5m),
            U("torr", "Torr", "Torr", 101325m / 760m),
            U("psi", "psi", "Pounds per square inch", 6894.757293168m),
            U("mmhg", "mmHg", "Millimeter of mercury (conventional)", 133.322387415m),
            U("in_hg", "inHg", "Inch of mercury (conventional)", 3386.389m));

        Add("energy", "Energy", "joule",
            U("joule", "J", "Joule", 1m),
            U("kilojoule", "kJ", "Kilojoule", 1000m),
            U("megajoule", "MJ", "Megajoule", 1000000m),
            U("watt_hour", "Wh", "Watt-hour", 3600m),
            U("kilowatt_hour", "kWh", "Kilowatt-hour", 3600000m),
            U("calorie_thermochemical", "cal", "Thermochemical calorie", 4.184m),
            U("kilocalorie", "kcal", "Kilocalorie", 4184m),
            U("btu_it", "BTU", "International Table BTU", 1055.05585262m),
            U("electronvolt", "eV", "Electronvolt", 1.602176634e-19m),
            U("erg", "erg", "Erg", 0.0000001m),
            U("foot_pound", "ft·lbf", "Foot-pound force", 1.3558179483314004m));

        Add("power", "Power", "watt",
            U("watt", "W", "Watt", 1m),
            U("kilowatt", "kW", "Kilowatt", 1000m),
            U("megawatt", "MW", "Megawatt", 1000000m),
            U("gigawatt", "GW", "Gigawatt", 1000000000m),
            U("milliwatt", "mW", "Milliwatt", 0.001m),
            U("mechanical_horsepower", "hp", "Mechanical horsepower", 745.6998715822702m),
            U("metric_horsepower", "PS", "Metric horsepower", 735.49875m),
            U("btu_per_hour", "BTU/h", "BTU per hour", 0.2930710701722222m));

        Add("force", "Force", "newton",
            U("newton", "N", "Newton", 1m),
            U("kilonewton", "kN", "Kilonewton", 1000m),
            U("dyne", "dyn", "Dyne", 0.00001m),
            U("pound_force", "lbf", "Pound-force", 4.4482216152605m),
            U("kilogram_force", "kgf", "Kilogram-force", 9.80665m));

        Add("torque", "Torque", "newton_meter",
            U("newton_meter", "N·m", "Newton meter", 1m),
            U("kilonewton_meter", "kN·m", "Kilonewton meter", 1000m),
            U("pound_foot", "lbf·ft", "Pound-foot", 1.3558179483314004m),
            U("pound_inch", "lbf·in", "Pound-inch", 0.1129848290276167m));

        Add("frequency", "Frequency", "hertz",
            U("hertz", "Hz", "Hertz", 1m),
            U("kilohertz", "kHz", "Kilohertz", 1000m),
            U("megahertz", "MHz", "Megahertz", 1000000m),
            U("gigahertz", "GHz", "Gigahertz", 1000000000m),
            U("rpm", "rpm", "Revolutions per minute", 1m / 60m),
            U("radians_per_second", "rad/s", "Radians per second", 1m / (2m * 3.1415926535897932384626433833m)));

        Add("angle", "Angle", "radian",
            U("radian", "rad", "Radian", 1m),
            U("degree", "°", "Degree", 3.1415926535897932384626433833m / 180m),
            U("arcminute", "′", "Arcminute", 3.1415926535897932384626433833m / 10800m),
            U("arcsecond", "″", "Arcsecond", 3.1415926535897932384626433833m / 648000m),
            U("gradian", "gon", "Gradian", 3.1415926535897932384626433833m / 200m),
            U("turn", "turn", "Full turn", 2m * 3.1415926535897932384626433833m));

        Add("data_storage", "Data storage", "byte",
            U("bit", "bit", "Bit", 0.125m),
            U("byte", "B", "Byte", 1m),
            U("kilobyte", "kB", "Kilobyte (decimal)", 1000m),
            U("megabyte", "MB", "Megabyte (decimal)", 1000000m),
            U("gigabyte", "GB", "Gigabyte (decimal)", 1000000000m),
            U("terabyte", "TB", "Terabyte (decimal)", 1000000000000m),
            U("petabyte", "PB", "Petabyte (decimal)", 1000000000000000m),
            U("kibibyte", "KiB", "Kibibyte (binary)", 1024m),
            U("mebibyte", "MiB", "Mebibyte (binary)", 1048576m),
            U("gibibyte", "GiB", "Gibibyte (binary)", 1073741824m),
            U("tebibyte", "TiB", "Tebibyte (binary)", 1099511627776m));

        Add("data_transfer_rate", "Data transfer rate", "bit_per_second",
            U("bit_per_second", "bit/s", "Bit per second", 1m),
            U("kilobit_per_second", "kbit/s", "Kilobit per second", 1000m),
            U("megabit_per_second", "Mbit/s", "Megabit per second", 1000000m),
            U("gigabit_per_second", "Gbit/s", "Gigabit per second", 1000000000m),
            U("byte_per_second", "B/s", "Byte per second", 8m),
            U("kilobyte_per_second", "kB/s", "Kilobyte per second", 8000m),
            U("megabyte_per_second", "MB/s", "Megabyte per second", 8000000m));

        Add("density", "Density", "kilogram_per_cubic_meter",
            U("kilogram_per_cubic_meter", "kg/m³", "Kilogram per cubic meter", 1m),
            U("gram_per_cubic_centimeter", "g/cm³", "Gram per cubic centimeter", 1000m),
            U("gram_per_liter", "g/L", "Gram per liter", 1m),
            U("pound_per_cubic_foot", "lb/ft³", "Pound per cubic foot", 16.01846337396m),
            U("pound_per_cubic_inch", "lb/in³", "Pound per cubic inch", 27679.9047102m));

        Add("fuel_economy", "Fuel economy", "kilometer_per_liter",
            U("kilometer_per_liter", "km/L", "Kilometer per liter", 1m),
            U("mile_per_gallon_us", "mpg (US)", "Miles per US gallon", 0.425143707430272m),
            U("mile_per_gallon_imperial", "mpg (imp)", "Miles per Imperial gallon", 0.354006189934647m),
            U("liter_per_100_kilometer", "L/100 km", "Liters per 100 kilometers", 1m));

        Add("flow_rate", "Volumetric flow rate", "cubic_meter_per_second",
            U("cubic_meter_per_second", "m³/s", "Cubic meter per second", 1m),
            U("liter_per_second", "L/s", "Liter per second", 0.001m),
            U("liter_per_minute", "L/min", "Liter per minute", 0.001m / 60m),
            U("cubic_meter_per_hour", "m³/h", "Cubic meter per hour", 1m / 3600m),
            U("gallon_us_per_minute", "US gal/min", "US gallons per minute", 0.003785411784m / 60m),
            U("cubic_foot_per_minute", "CFM", "Cubic feet per minute", 0.028316846592m / 60m));

        Add("dynamic_viscosity", "Dynamic viscosity", "pascal_second",
            U("pascal_second", "Pa·s", "Pascal second", 1m),
            U("millipascal_second", "mPa·s", "Millipascal second", 0.001m),
            U("poise", "P", "Poise", 0.1m),
            U("centipoise", "cP", "Centipoise", 0.001m),
            U("pound_per_foot_second", "lb/(ft·s)", "Pound per foot second", 1.48816394357m));

        Add("kinematic_viscosity", "Kinematic viscosity", "square_meter_per_second",
            U("square_meter_per_second", "m²/s", "Square meter per second", 1m),
            U("stokes", "St", "Stokes", 0.0001m),
            U("centistokes", "cSt", "Centistokes", 0.000001m),
            U("square_foot_per_second", "ft²/s", "Square foot per second", 0.09290304m));

        Add("electric_current", "Electric current", "ampere",
            U("ampere", "A", "Ampere", 1m),
            U("milliampere", "mA", "Milliampere", 0.001m),
            U("microampere", "µA", "Microampere", 0.000001m),
            U("kiloampere", "kA", "Kiloampere", 1000m));

        Add("voltage", "Voltage", "volt",
            U("volt", "V", "Volt", 1m),
            U("millivolt", "mV", "Millivolt", 0.001m),
            U("microvolt", "µV", "Microvolt", 0.000001m),
            U("kilovolt", "kV", "Kilovolt", 1000m),
            U("megavolt", "MV", "Megavolt", 1000000m));

        Add("resistance", "Electrical resistance", "ohm",
            U("ohm", "Ω", "Ohm", 1m),
            U("milliohm", "mΩ", "Milliohm", 0.001m),
            U("kiloohm", "kΩ", "Kiloohm", 1000m),
            U("megaohm", "MΩ", "Megaohm", 1000000m));

        Add("conductance", "Electrical conductance", "siemens",
            U("siemens", "S", "Siemens", 1m),
            U("millisiemens", "mS", "Millisiemens", 0.001m),
            U("microsiemens", "µS", "Microsiemens", 0.000001m));

        Add("electric_charge", "Electric charge", "coulomb",
            U("coulomb", "C", "Coulomb", 1m),
            U("millicoulomb", "mC", "Millicoulomb", 0.001m),
            U("microcoulomb", "µC", "Microcoulomb", 0.000001m),
            U("ampere_hour", "Ah", "Ampere-hour", 3600m),
            U("milliampere_hour", "mAh", "Milliampere-hour", 3.6m));

        Add("capacitance", "Capacitance", "farad",
            U("farad", "F", "Farad", 1m),
            U("millifarad", "mF", "Millifarad", 0.001m),
            U("microfarad", "µF", "Microfarad", 0.000001m),
            U("nanofarad", "nF", "Nanofarad", 0.000000001m),
            U("picofarad", "pF", "Picofarad", 0.000000000001m));

        Add("inductance", "Inductance", "henry",
            U("henry", "H", "Henry", 1m),
            U("millihenry", "mH", "Millihenry", 0.001m),
            U("microhenry", "µH", "Microhenry", 0.000001m));

        Add("magnetic_flux", "Magnetic flux", "weber",
            U("weber", "Wb", "Weber", 1m),
            U("milliweber", "mWb", "Milliweber", 0.001m),
            U("maxwell", "Mx", "Maxwell", 0.00000001m));

        Add("magnetic_flux_density", "Magnetic flux density", "tesla",
            U("tesla", "T", "Tesla", 1m),
            U("millitesla", "mT", "Millitesla", 0.001m),
            U("microtesla", "µT", "Microtesla", 0.000001m),
            U("gauss", "G", "Gauss", 0.0001m));

        Add("illuminance", "Illuminance", "lux",
            U("lux", "lx", "Lux", 1m),
            U("foot_candle", "fc", "Foot-candle", 10.7639104167097m),
            U("phot", "ph", "Phot", 10000m));

        Add("luminous_flux", "Luminous flux", "lumen",
            U("lumen", "lm", "Lumen", 1m),
            U("kilolumen", "klm", "Kilolumen", 1000m));

        Add("luminous_intensity", "Luminous intensity", "candela",
            U("candela", "cd", "Candela", 1m),
            U("millicandela", "mcd", "Millicandela", 0.001m));

        Add("amount_of_substance", "Amount of substance", "mole",
            U("mole", "mol", "Mole", 1m),
            U("millimole", "mmol", "Millimole", 0.001m),
            U("micromole", "µmol", "Micromole", 0.000001m),
            U("kilomole", "kmol", "Kilomole", 1000m));

        Add("molar_concentration", "Molar concentration", "mole_per_cubic_meter",
            U("mole_per_cubic_meter", "mol/m³", "Mole per cubic meter", 1m),
            U("mole_per_liter", "mol/L", "Mole per liter", 1000m),
            U("millimole_per_liter", "mmol/L", "Millimole per liter", 1m),
            U("micromole_per_liter", "µmol/L", "Micromole per liter", 0.001m));

        Add("radiation_absorbed_dose", "Radiation absorbed dose", "gray",
            U("gray", "Gy", "Gray", 1m),
            U("milligray", "mGy", "Milligray", 0.001m),
            U("rad", "rad", "Rad", 0.01m));

        Add("radiation_equivalent_dose", "Radiation equivalent dose", "sievert",
            U("sievert", "Sv", "Sievert", 1m),
            U("millisievert", "mSv", "Millisievert", 0.001m),
            U("microsievert", "µSv", "Microsievert", 0.000001m),
            U("rem", "rem", "Rem", 0.01m));

        Add("catalytic_activity", "Catalytic activity", "katal",
            U("katal", "kat", "Katal", 1m),
            U("mole_per_second", "mol/s", "Mole per second", 1m),
            U("enzyme_unit", "U", "Enzyme unit", 1m / 60000000m));

        Add("sound_pressure", "Sound pressure", "pascal",
            U("pascal", "Pa", "Pascal", 1m),
            U("micro_pascal", "µPa", "Micropascal", 0.000001m),
            U("bar", "bar", "Bar", 100000m));
    }

    public IReadOnlyCollection<UnitCategoryDefinition> Categories => _categories.Values;

    public bool TryGetCategory(string id, out UnitCategoryDefinition category) =>
        _categories.TryGetValue(id, out category!);

    public UnitCategoryDefinition GetCategory(string id)
    {
        if (!_categories.TryGetValue(id, out var category))
            throw new UnknownCategoryException(id);

        return category;
    }

    private void Add(string id, string name, string baseUnit, params UnitDefinition[] units)
    {
        var map = new Dictionary<string, UnitDefinition>(StringComparer.OrdinalIgnoreCase);
        foreach (var unit in units)
        {
            if (!map.TryAdd(unit.Id, unit))
                throw new InvalidOperationException($"Duplicate unit '{unit.Id}' in '{id}'.");
        }

        if (!map.ContainsKey(baseUnit))
            throw new InvalidOperationException($"Base unit '{baseUnit}' missing from '{id}'.");

        _categories.Add(id, new UnitCategoryDefinition(id, name, baseUnit, map));
    }

    private static UnitDefinition U(string id, string symbol, string description,
        decimal factor, decimal offset = 0m) =>
        new(id, symbol, description, factor, offset);
}

public sealed class UnknownCategoryException(string category)
    : ArgumentException($"Unsupported category '{category}'.")
{
    public string Category { get; } = category;
}

public sealed class UnknownUnitException(string category, string unit)
    : ArgumentException($"Unit '{unit}' is not supported for category '{category}'.")
{
    public string Category { get; } = category;
    public string Unit { get; } = unit;
}
