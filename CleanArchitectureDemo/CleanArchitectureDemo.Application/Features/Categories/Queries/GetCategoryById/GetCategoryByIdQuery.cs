using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Queries/GetCategoryById/GetCategoryByIdQuery.cs
using MediatR;
using System;

namespace CleanArchitectureDemo.Application.Features.Categories.Queries.GetCategoryById;

// Query que lleva el ID de la categoría a buscar.
// Devuelve un CategoryDto o null si no se encuentra.
public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;