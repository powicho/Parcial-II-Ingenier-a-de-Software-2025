using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Commands/UpdateProduct/UpdateProductCommand.cs
using MediatR;
using System;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.UpdateProduct;

// Comando para actualizar. Necesita el ID y los nuevos datos.
// IRequest<bool> indica que devolverá true si tuvo éxito, false si no encontró el producto.
public record UpdateProductCommand(
    Guid Id, // ID del producto a actualizar
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId) : IRequest<bool>;