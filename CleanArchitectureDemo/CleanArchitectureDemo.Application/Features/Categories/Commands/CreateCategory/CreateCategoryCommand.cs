using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Commands/CreateCategory/CreateCategoryCommand.cs
using MediatR;
using CleanArchitectureDemo.Application.Features.Categories.Queries; // Para CategoryDto
using System;

namespace CleanArchitectureDemo.Application.Features.Categories.Commands.CreateCategory;

// Comando con el nombre para crear la categoría.
// Devuelve el DTO de la categoría creada.
public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;