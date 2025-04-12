using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Commands/UpdateCategory/UpdateCategoryCommand.cs
using MediatR;
using System;

namespace CleanArchitectureDemo.Application.Features.Categories.Commands.UpdateCategory;

// Comando para actualizar. Necesita el ID y el nuevo nombre.
// Devuelve true si tuvo éxito, false si no encontró la categoría.
public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<bool>;