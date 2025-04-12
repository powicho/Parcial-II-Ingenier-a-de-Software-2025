using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Commands/UpdateProduct/UpdateProductCommandHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar el producto existente por ID
        var productToUpdate = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        // 2. Si no existe, devolver false (o podrías lanzar una excepción)
        if (productToUpdate == null)
        {
            return false; // No encontrado
        }

        // 3. Usar el método Update de la entidad para aplicar los cambios
        productToUpdate.Update(
            request.Name,
            request.Description,
            request.Price,
            request.CategoryId);

        // 4. Indicar al repositorio que la entidad ha sido modificada (EF Core a veces lo detecta solo)
        _productRepository.Update(productToUpdate); // Llama al método Update del repositorio

        // 5. Guardar los cambios en la base de datos
        await _productRepository.SaveChangesAsync(cancellationToken);

        return true; // Éxito
    }
}