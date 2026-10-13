using Microsoft.AspNetCore.Mvc;
using UnitConverter.Api.Contracts;
using UnitConverter.Api.Conversion;

namespace UnitConverter.Api.Controllers;

[ApiController]
[Route("api/categories")]
[Produces("application/json")]
public sealed class CategoriesController(UnitCatalog catalog) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CategoryResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<CategoryResponse>> GetCategories()
    {
        var categories = catalog.Categories
            .OrderBy(c => c.Name)
            .Select(ToResponse)
            .ToArray();
        return Ok(categories);
    }

    [HttpGet("{categoryId}/units")]
    [ProducesResponseType<CategoryResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoryResponse> GetUnits(string categoryId) =>
        ToResponse(catalog.GetCategory(categoryId));

    private static CategoryResponse ToResponse(UnitCategoryDefinition category) =>
        new(category.Id, category.Name, category.BaseUnit,
            category.Units.Values
                .OrderBy(u => u.Description)
                .Select(u => new UnitResponse(u.Id, u.Symbol, u.Description))
                .ToArray());
}
