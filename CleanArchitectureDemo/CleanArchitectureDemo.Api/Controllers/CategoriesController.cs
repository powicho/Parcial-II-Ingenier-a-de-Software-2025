// Este archivo va en: src/Interface/CleanArchitectureDemo.Api/Controllers/CategoriesController.cs
using MediatR; // Para ISender
using Microsoft.AspNetCore.Mvc; // Para [ApiController], [Route], etc.
using CleanArchitectureDemo.Application.Features.Categories.Commands.CreateCategory; // Command de Crear
using CleanArchitectureDemo.Application.Features.Categories.Queries.GetAllCategories; // Query de Obtener Todas
using CleanArchitectureDemo.Application.Features.Categories.Queries.GetCategoryById; // Query de Obtener por ID
using CleanArchitectureDemo.Application.Features.Categories.Commands.UpdateCategory; // Command de Actualizar
using CleanArchitectureDemo.Application.Features.Categories.Commands.DeleteCategory; // Command de Eliminar
using CleanArchitectureDemo.Application.Features.Categories.Queries; // Para CategoryDto
using System; // Para Guid
using System.Collections.Generic; // Para IEnumerable
using System.Threading.Tasks; // Para Task<>
using System.Threading; // Para CancellationToken

namespace CleanArchitectureDemo.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Ruta base: /api/categories
public class CategoriesController : ControllerBase
{
    private readonly ISender _mediator;

    public CategoriesController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // --- GET: api/categories ---
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    // --- GET: api/categories/{id} ---
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }

    // --- POST: api/categories ---
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        // Usamos un 'CreateCategoryRequest' para desacoplar del comando si queremos añadir validación aquí
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Category name is required.");
        }
        var command = new CreateCategoryCommand(request.Name);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
    }

    // --- PUT: api/categories/{id} ---
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty || string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Valid category ID and name are required.");
        }
        var command = new UpdateCategoryCommand(id, request.Name);
        var success = await _mediator.Send(command, cancellationToken);
        return success ? NoContent() : NotFound();
    }

    // --- DELETE: api/categories/{id} ---
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid category ID.");
        }
        var command = new DeleteCategoryCommand(id);
        var success = await _mediator.Send(command, cancellationToken);
        return success ? NoContent() : NotFound();
    }
}

// Records simples para definir el cuerpo esperado de las peticiones POST y PUT
public record CreateCategoryRequest(string Name);
public record UpdateCategoryRequest(string Name);