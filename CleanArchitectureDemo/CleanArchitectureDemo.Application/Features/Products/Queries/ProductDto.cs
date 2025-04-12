using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Queries/ProductDto.cs
namespace CleanArchitectureDemo.Application.Features.Products.Queries;

// Objeto simple para transferir datos de Producto hacia afuera (ej: a la API)
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    // Podríamos añadir CategoryName aquí si hacemos un Join en la consulta
}