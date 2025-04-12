using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/Interfaces/Persistence/ICategoryRepository.cs
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

// Define qué operaciones queremos hacer con las Categorías en la base de datos
public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
    void Update(Category category); // La actualización suele ser manejada por el tracker de EF Core
    void Delete(Category category); // La eliminación también
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); // Para guardar los cambios
}
