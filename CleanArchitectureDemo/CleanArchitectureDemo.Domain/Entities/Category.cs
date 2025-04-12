using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Core/CleanArchitectureDemo.Domain/Entities/Category.cs
namespace CleanArchitectureDemo.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    // Constructor privado necesario para herramientas como Entity Framework
    private Category() { }

    // Método 'fábrica' para crear categorías de forma controlada
    public static Category Create(string name)
    {
        // Validación simple: el nombre no puede estar vacío
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));

        // Creamos la nueva categoría con un ID nuevo
        return new Category { Id = Guid.NewGuid(), Name = name };
    }

    // Método para actualizar el nombre de una categoría existente
    public void Update(string name)
    {
        // Misma validación que al crear
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));
        Name = name;
    }
}
