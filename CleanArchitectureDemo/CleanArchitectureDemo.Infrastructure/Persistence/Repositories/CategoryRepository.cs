// Este archivo va en: src/Infrastructure/CleanArchitectureDemo.Infrastructure/Persistence/Repositories/CategoryRepository.cs
using Microsoft.EntityFrameworkCore; // Necesario para ToListAsync, etc.
using CleanArchitectureDemo.Domain.Entities; // Usa la entidad Category
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence;

namespace CleanArchitectureDemo.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    // Necesita el DbContext para hablar con la base de datos
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Implementación de los métodos de la interfaz ICategoryRepository
    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // FindAsync es eficiente para buscar por clave primaria
        return await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // Obtiene todas las categorías de la tabla y las convierte en una lista
        return await _context.Categories.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        // Añade la nueva categoría al contexto (aún no está en la DB)
        await _context.Categories.AddAsync(category, cancellationToken);
    }

    public void Update(Category category)
    {
        // EF Core rastrea cambios. Si obtuviste la categoría del contexto y la modificaste,
        // SaveChangesAsync es suficiente. Marcarla como 'Modified' es más explícito si está "desconectada".
        _context.Entry(category).State = EntityState.Modified;
    }

    public void Delete(Category category)
    {
        // Marca la categoría para ser eliminada en el próximo SaveChangesAsync
        _context.Categories.Remove(category);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Guarda todos los cambios pendientes (Adds, Updates, Deletes) en la base de datos real
        return await _context.SaveChangesAsync(cancellationToken);
    }
}