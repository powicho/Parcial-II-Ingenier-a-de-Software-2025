using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Commands/DeleteCategory/DeleteCategoryCommand.cs
using MediatR;
using System;

namespace CleanArchitectureDemo.Application.Features.Categories.Commands.DeleteCategory;

// Comando para eliminar. Solo necesita el ID.
// Devuelve bool: true si se eliminó, false si no se encontró.
public record DeleteCategoryCommand(Guid Id) : IRequest<bool>;