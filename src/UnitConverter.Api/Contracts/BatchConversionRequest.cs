using System.ComponentModel.DataAnnotations;

namespace UnitConverter.Api.Contracts;

public sealed record BatchConversionRequest
{
    [Required, MinLength(1), MaxLength(100)]
    public List<ConversionRequest> Conversions { get; init; } = [];
}

public sealed record BatchConversionResponse(
    IReadOnlyList<ConversionResponse> Results);
