using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Queries/CategoryDto.cs
namespace CleanArchitectureDemo.Application.Features.Categories.Queries;

// Objeto simple para devolver datos de Categoría
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}