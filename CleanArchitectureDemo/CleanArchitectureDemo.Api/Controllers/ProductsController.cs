// Este archivo va en: src/Interface/CleanArchitectureDemo.Api/Controllers/ProductsController.cs
using MediatR; // Para poder enviar comandos y queries
using Microsoft.AspNetCore.Mvc; // Para [ApiController], [Route], IActionResult, etc.
using CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;
using CleanArchitectureDemo.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitectureDemo.Application.Features.Products.Queries.GetProductById;
using CleanArchitectureDemo.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitectureDemo.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitectureDemo.Application.Features.Products.Queries; // Para ProductDto
using System; // Para Guid
using System.Collections.Generic; // Para IEnumerable
using System.Threading.Tasks; // Para Task<>
using System.Threading; // Para CancellationToken

namespace CleanArchitectureDemo.Api.Controllers;

[ApiController] // Indica que es un controlador de API (habilita características automáticas)
[Route("api/[controller]")] // Define la ruta base: /api/products
public class ProductsController : ControllerBase // Hereda de ControllerBase (base para APIs)
{
    // Necesitamos MediatR para enviar los comandos/queries a la capa Application
    private readonly ISender _mediator;

    // El ISender se inyecta automáticamente gracias al registro en Program.cs
    public ProductsController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // --- GET: api/products ---
    // Obtiene todos los productos
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)] // Documenta la respuesta exitosa para Swagger
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var query = new GetAllProductsQuery(); // Crea el objeto Query
        var result = await _mediator.Send(query, cancellationToken); // Envía la query a su Handler en Application
        return Ok(result); // Devuelve 200 OK con la lista de productos
    }

    // --- GET: api/products/{id} ---
    // Obtiene un producto por su ID
    [HttpGet("{id:guid}")] // La ruta incluye el ID (restringido a formato GUID)
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Documenta el caso de "no encontrado"
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id); // Crea la query con el ID
        var result = await _mediator.Send(query, cancellationToken); // Envía

        // Si el handler devuelve null, significa que no se encontró
        return result is not null ? Ok(result) : NotFound(); // Devuelve 200 OK o 404 Not Found
    }

    // --- POST: api/products ---
    // Crea un nuevo producto
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)] // Documenta la respuesta de creación exitosa (201)
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Documenta el caso de datos inválidos
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        // [FromBody] indica que los datos vienen en el cuerpo de la petición HTTP (JSON)
        // ASP.NET Core + MediatR pueden hacer validaciones automáticas si configuras FluentValidation,
        // de lo contrario, podrías validar el 'command' aquí.

        var result = await _mediator.Send(command, cancellationToken); // Envía el comando de creación

        // Devuelve 201 Created. Incluye la URL para obtener el nuevo producto (buenas prácticas REST)
        // y también devuelve el producto creado en el cuerpo.
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    // --- PUT: api/products/{id} ---
    // Actualiza un producto existente
    [HttpPut("{id:guid}")] // Usa el ID en la ruta
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Respuesta estándar para PUT/DELETE exitoso sin contenido
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        // Usamos un 'UpdateProductRequest' para el cuerpo para no mezclar el ID de la ruta y el cuerpo.
        if (id == Guid.Empty) // Validación básica
        {
            return BadRequest("Invalid product ID.");
        }

        // Creamos el comando real para MediatR usando el ID de la ruta y los datos del cuerpo
        var command = new UpdateProductCommand(id, request.Name, request.Description, request.Price, request.CategoryId);

        var success = await _mediator.Send(command, cancellationToken); // El handler devuelve true/false

        return success ? NoContent() : NotFound(); // Devuelve 204 No Content si tuvo éxito, 404 si no
    }

    // --- DELETE: api/products/{id} ---
    // Elimina un producto
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid product ID.");
        }

        var command = new DeleteProductCommand(id); // Crea el comando de eliminación
        var success = await _mediator.Send(command, cancellationToken); // Envía

        return success ? NoContent() : NotFound(); // Devuelve 204 si OK, 404 si no encontrado
    }
}

// Un objeto simple (record) para definir la estructura esperada en el cuerpo de la petición PUT.
// Ayuda a separar lo que viene en la petición de lo que usa el comando interno.
public record UpdateProductRequest(string Name, string? Description, decimal Price, Guid CategoryId);