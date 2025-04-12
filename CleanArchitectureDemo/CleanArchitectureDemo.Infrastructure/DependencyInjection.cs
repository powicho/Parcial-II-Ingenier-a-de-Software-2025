using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Infrastructure/CleanArchitectureDemo.Infrastructure/DependencyInjection.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration; // Para leer configuración (ej: connection strings)
using Microsoft.EntityFrameworkCore; // Para UseInMemoryDatabase o UseSqlServer, etc.
using CleanArchitectureDemo.Infrastructure.Persistence; // Para ApplicationDbContext
using CleanArchitectureDemo.Infrastructure.Persistence.Repositories;
using CleanArchitectureDemo.Application.Features.Products.Queries.Interfaces.Persistence; // Para las implementaciones de Repositorio

namespace CleanArchitectureDemo.Infrastructure;

public static class DependencyInjection
{
    // Método de extensión para registrar todo lo de esta capa
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration) // Recibe la configuración general de la app
    {
        // --- Configuración de la Base de Datos ---
        // Para este ejemplo, usamos una base de datos EN MEMORIA.
        // ¡Esto es genial para probar rápido, pero los datos se pierden al cerrar la app!
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CleanArchDemoDb")); // El nombre es inventado

        /* --- SI QUISIERAS USAR SQL SERVER (Ejemplo comentado) ---
        // 1. Necesitarías instalar el paquete NuGet: Microsoft.EntityFrameworkCore.SqlServer
        // 2. Añadir tu cadena de conexión en appsettings.json (en el proyecto API)
        // var connectionString = configuration.GetConnectionString("DefaultConnection");
        // services.AddDbContext<ApplicationDbContext>(options =>
        //     options.UseSqlServer(connectionString,
        //         builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        // ----------------------------------------------------- */

        // --- Registro de Repositorios ---
        // Aquí le decimos a .NET: "Cuando alguien pida un IProductRepository,
        // créale una instancia de ProductRepository".
        // AddScoped significa que se crea una instancia por cada petición HTTP que llega a la API.
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // Aquí registrarías otros servicios de infraestructura si los tuvieras
        // (ej: un servicio para enviar emails, un cliente para otra API, etc.)
        // services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}