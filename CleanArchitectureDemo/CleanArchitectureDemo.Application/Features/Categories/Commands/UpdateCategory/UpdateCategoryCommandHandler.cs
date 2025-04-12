using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Commands/UpdateCategory/UpdateCategoryCommandHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

namespace CleanArchitectureDemo.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Buscar la categoría existente
        var categoryToUpdate = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (categoryToUpdate == null)
        {
            return false; // No encontrada
        }

        // Usar el método Update de la entidad
        categoryToUpdate.Update(request.Name);

        // Indicar al repositorio que se modificó (por si acaso)
        _categoryRepository.Update(categoryToUpdate);

        // Guardar cambios
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return true; // Éxito
    }
}