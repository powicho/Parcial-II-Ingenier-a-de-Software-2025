using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Commands/DeleteCategory/DeleteCategoryCommandHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

namespace CleanArchitectureDemo.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        // Buscar la categoría a eliminar
        var categoryToDelete = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (categoryToDelete == null)
        {
            return false; // No encontrada
        }

        // Indicar al repositorio que elimine
        _categoryRepository.Delete(categoryToDelete);

        // Guardar cambios
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return true; // Éxito
    }
}