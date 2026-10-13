# Universal Unit Converter API

Engineering-focused, stateless unit-conversion REST API built with ASP.NET Core 10, C#, `decimal` arithmetic, a data-driven unit registry, and xUnit tests.

## Features

- 30+ measurement categories and a broad set of SI, customary, scientific, and engineering units.
- `decimal` arithmetic for predictable high-precision conversions.
- Linear conversions via canonical base units; dedicated affine conversion for temperature and reciprocal conversion for fuel economy.
- Category and unit discovery endpoints.
- Single and batch conversions.
- Request validation, consistent Problem Details responses, and OpenAPI document support.
- No database and no third-party runtime dependencies.
- xUnit test project for conversion accuracy, reverse conversions, edge cases, and validation.

> Scope note: "all units" is not a finite universal list. This project provides a broad engineering catalogue and an extensible registry. Some quantities require contextual data (for example, volume-to-mass needs density), logarithmic units need domain-specific reference values, and currency needs an external rate source; these are intentionally not treated as ordinary unit conversions.

## Requirements

- .NET 10 SDK
- Optional: `curl`, Postman, or an HTTP client

## Run

From this directory:

```bash
dotnet restore
dotnet test
dotnet run --project src/UnitConverter.Api
```

The API prints its local HTTP/HTTPS addresses on startup. Open `/openapi/v1.json` for the OpenAPI document in Development. If you want a browser UI, add Swagger UI via Swashbuckle; the project intentionally keeps runtime dependencies to the ASP.NET Core shared framework.

## Endpoints

| Method | Route | Description |
|---|---|---|
| GET | `/health` | Liveness check |
| GET | `/api/categories` | List all categories |
| GET | `/api/categories/{categoryId}/units` | List units for one category |
| POST | `/api/conversions` | Convert one value |
| POST | `/api/conversions/batch` | Convert up to 100 values |

### Single conversion

```http
POST /api/conversions
Content-Type: application/json
```

```json
{
  "value": 100,
  "category": "length",
  "fromUnit": "meter",
  "toUnit": "foot"
}
```

Example response:

```json
{
  "inputValue": 100,
  "category": "length",
  "fromUnit": "meter",
  "toUnit": "foot",
  "result": 328.08398950131233595800524934
}
```

### Temperature

```json
{
  "value": 100,
  "category": "temperature",
  "fromUnit": "celsius",
  "toUnit": "fahrenheit"
}
```

Result: `212`.

### Batch conversion

```http
POST /api/conversions/batch
Content-Type: application/json
```

```json
{
  "conversions": [
    { "value": 1, "category": "length", "fromUnit": "meter", "toUnit": "foot" },
    { "value": 25, "category": "temperature", "fromUnit": "celsius", "toUnit": "fahrenheit" }
  ]
}
```

The batch endpoint is atomic: if any item is invalid, the request returns a validation error rather than a partial result.

## Unit IDs and conventions

- Unit IDs are case-insensitive, but use the IDs returned by the units endpoint.
- `byte` and its decimal/binary multiples are distinct (`kilobyte` = 1000 bytes; `kibibyte` = 1024 bytes).
- Volume uses US customary gallons/cups unless a unit ID explicitly says `imperial`.
- Tonnes are metric tonnes; `ton_us` and `ton_imperial` are separate.
- Month and year are approximate duration units (30 days and 365.25 days respectively); they are not calendar arithmetic.
- Fuel economy is reciprocal. For example, mpg and L/100 km are converted with the reciprocal formula, not a linear factor.
- Angles use radians as the canonical base.
- This project does not silently convert between dimensions.

## Add a unit or category

Edit `src/UnitConverter.Api/Conversion/UnitCatalog.cs`. Linear units use a multiplier to the category's canonical unit. Keep factors as `decimal` literals where possible. For a new non-linear dimension, add a dedicated converter in `Conversion/ConversionEngine.cs` and test it in `tests/UnitConverter.Tests`.

## Project layout

```text
src/UnitConverter.Api/
  Controllers/
  Contracts/
  Conversion/
  Middleware/
  Program.cs
tests/UnitConverter.Tests/
```
