using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Infrastructure/CleanArchitectureDemo.Infrastructure/Persistence/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using CleanArchitectureDemo.Domain.Entities; // Necesita conocer las entidades
using System.Reflection; // Para configuraciones (opcional)

namespace CleanArchitectureDemo.Infrastructure.Persistence;

// Hereda de DbContext de Entity Framework Core
public class ApplicationDbContext : DbContext
{
    // Constructor que recibe las opciones de configuración (como qué DB usar)
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet representa una tabla en la base de datos para cada entidad
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    // Método para configurar el modelo (relaciones, tipos de columna, etc.)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica configuraciones de entidad si las tuvieras en archivos separados
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // O configura directamente aquí:
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id); // Clave primaria
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // Campo obligatorio, max 100 chars
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // Tipo específico para dinero
            // Podríamos definir la relación con Category aquí, pero EF Core suele inferirla por CategoryId
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        });

        base.OnModelCreating(modelBuilder);
    }
}