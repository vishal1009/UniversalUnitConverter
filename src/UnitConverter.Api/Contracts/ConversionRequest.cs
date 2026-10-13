using System.ComponentModel.DataAnnotations;

namespace UnitConverter.Api.Contracts;

public sealed record ConversionRequest
{
    [Range(typeof(decimal), "-79228162514264337593543950335", "79228162514264337593543950335")]
    public decimal Value { get; init; }

    [Required, StringLength(80, MinimumLength = 1)]
    public string Category { get; init; } = string.Empty;

    [Required, StringLength(80, MinimumLength = 1)]
    public string FromUnit { get; init; } = string.Empty;

    [Required, StringLength(80, MinimumLength = 1)]
    public string ToUnit { get; init; } = string.Empty;
}
