using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Commands/DeleteProduct/DeleteProductCommand.cs
using MediatR;
using System;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.DeleteProduct;

// Comando para eliminar. Solo necesita el ID.
// Devuelve bool: true si se eliminó, false si no se encontró.
public record DeleteProductCommand(Guid Id) : IRequest<bool>;