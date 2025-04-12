using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Commands/CreateProduct/CreateProductCommand.cs
using MediatR;
using CleanArchitectureDemo.Application.Features.Products.Queries; // Necesitamos ProductDto
using System;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;

// Comando con los datos necesarios para crear un producto.
// Devuelve el DTO del producto recién creado.
public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId) : IRequest<ProductDto>;