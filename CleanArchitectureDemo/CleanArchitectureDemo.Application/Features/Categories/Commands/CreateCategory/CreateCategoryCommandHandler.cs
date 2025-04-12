using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Categories/Commands/CreateCategory/CreateCategoryCommandHandler.cs
using MediatR;
using CleanArchitectureDemo.Domain.Entities; // Para Category
using CleanArchitectureDemo.Application.Features.Categories.Queries; // Para CategoryDto
using System.Threading;
using System.Threading.Tasks;
using System;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

namespace CleanArchitectureDemo.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Crear la entidad de dominio
        var category = Category.Create(request.Name);

        // Añadir al repositorio
        await _categoryRepository.AddAsync(category, cancellationToken);

        // Guardar cambios
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        // Mapear a DTO para devolver
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}