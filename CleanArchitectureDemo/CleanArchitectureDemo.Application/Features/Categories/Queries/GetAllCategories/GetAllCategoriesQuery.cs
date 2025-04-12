using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Queries/GetAllCategories/GetAllCategoriesQuery.cs
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Categories.Queries.GetAllCategories;

// Query simple para obtener todas las categorías.
// Devuelve una lista (IEnumerable) de CategoryDto.
public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;