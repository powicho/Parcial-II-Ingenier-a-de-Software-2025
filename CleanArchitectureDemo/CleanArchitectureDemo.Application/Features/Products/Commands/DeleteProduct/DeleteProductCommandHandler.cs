using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Commands/DeleteProduct/DeleteProductCommandHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar el producto a eliminar
        var productToDelete = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        // 2. Si no existe, devolver false
        if (productToDelete == null)
        {
            return false; // No encontrado
        }

        // 3. Indicar al repositorio que elimine la entidad
        _productRepository.Delete(productToDelete);

        // 4. Guardar cambios
        await _productRepository.SaveChangesAsync(cancellationToken);

        return true; // Éxito
    }
}