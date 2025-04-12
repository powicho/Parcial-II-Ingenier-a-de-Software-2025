using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Core/CleanArchitectureDemo.Domain/Entities/Product.cs
namespace CleanArchitectureDemo.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; } // Usamos Guid como ID único global
    public string Name { get; private set; } = string.Empty; // Nombre obligatorio
    public string? Description { get; private set; } // Descripción opcional (puede ser null)
    public decimal Price { get; private set; } // Precio, debe ser positivo
    public Guid CategoryId { get; private set; } // Para saber a qué categoría pertenece

    // Constructor privado para herramientas
    private Product() { }

    // Método 'fábrica' para crear productos
    public static Product Create(string name, string? description, decimal price, Guid categoryId)
    {
        // Validaciones de las reglas del negocio
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));
        if (price <= 0)
            throw new ArgumentException("Product price must be positive.", nameof(price));
        if (categoryId == Guid.Empty)
            throw new ArgumentException("Category ID must be provided.", nameof(categoryId));


        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            CategoryId = categoryId
        };
    }

    // Método para actualizar un producto existente
    public void Update(string name, string? description, decimal price, Guid categoryId)
    {
        // Mismas validaciones que al crear
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));
        if (price <= 0)
            throw new ArgumentException("Product price must be positive.", nameof(price));
        if (categoryId == Guid.Empty)
            throw new ArgumentException("Category ID must be provided.", nameof(categoryId));

        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }
}