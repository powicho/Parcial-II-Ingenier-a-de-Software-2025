using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Commands/CreateProduct/CreateProductCommandHandler.cs
using MediatR;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Application.Features.Products.Queries; // ProductDto
using System.Threading;
using System.Threading.Tasks;
using System;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence; // Para ArgumentNullException

namespace CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    // Podrías inyectar ICategoryRepository aquí si necesitas validar que CategoryId existe

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        // Guarda la referencia al repositorio que nos pasen
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad de dominio usando el método fábrica
        var product = Product.Create(
            request.Name,
            request.Description,
            request.Price,
            request.CategoryId);

        // 2. Añadir la entidad al repositorio (aún no se guarda en DB)
        await _productRepository.AddAsync(product, cancellationToken);

        // 3. Guardar los cambios en la base de datos
        await _productRepository.SaveChangesAsync(cancellationToken);

        // 4. Mapear la entidad creada a un DTO para devolverlo
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId
        };
    }
}