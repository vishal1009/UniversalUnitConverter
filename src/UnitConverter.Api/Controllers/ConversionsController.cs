using Microsoft.AspNetCore.Mvc;
using UnitConverter.Api.Contracts;
using UnitConverter.Api.Conversion;

namespace UnitConverter.Api.Controllers;

[ApiController]
[Route("api/conversions")]
[Produces("application/json")]
public sealed class ConversionsController(ConversionEngine engine) : ControllerBase
{
    /// <summary>Convert one numeric value between units in the same category.</summary>
    [HttpPost]
    [ProducesResponseType<ConversionResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public ActionResult<ConversionResponse> Convert([FromBody] ConversionRequest request)
    {
        var result = engine.Convert(request.Value, request.Category, request.FromUnit, request.ToUnit);
        return Ok(new ConversionResponse(request.Value, request.Category,
            request.FromUnit, request.ToUnit, result));
    }

    /// <summary>Convert multiple values in one request. The batch is atomic.</summary>
    [HttpPost("batch")]
    [ProducesResponseType<BatchConversionResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public ActionResult<BatchConversionResponse> ConvertBatch(
        [FromBody] BatchConversionRequest request)
    {
        var results = request.Conversions.Select(item =>
            new ConversionResponse(item.Value, item.Category, item.FromUnit, item.ToUnit,
                engine.Convert(item.Value, item.Category, item.FromUnit, item.ToUnit)))
            .ToArray();

        return Ok(new BatchConversionResponse(results));
    }
}
