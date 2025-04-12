// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Features/Products/Queries/GetProductById/GetProductByIdQueryHandler.cs
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

namespace CleanArchitectureDemo.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            return null; // No encontrado
        }

        // Mapear a DTO
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