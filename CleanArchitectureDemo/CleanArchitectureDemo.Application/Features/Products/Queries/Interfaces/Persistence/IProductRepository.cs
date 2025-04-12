using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Interfaces/Persistence/IProductRepository.cs
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

// Define qué operaciones queremos hacer con los Productos en la base de datos
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    void Update(Product product);
    void Delete(Product product);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}