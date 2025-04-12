using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Este archivo va en: src/Application/CleanArchitectureDemo.Application/DependencyInjection.cs
using Microsoft.Extensions.DependencyInjection;
using System.Reflection; // Para Assembly.GetExecutingAssembly()

namespace CleanArchitectureDemo.Application;

// Clase estática para organizar el registro de servicios de esta capa
public static class DependencyInjection
{
    // Método de extensión para IServiceCollection (el contenedor de dependencias de .NET)
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registra MediatR y busca automáticamente todos los Handlers (como CreateProductCommandHandler)
        // en este proyecto (Assembly).
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Aquí también podrías registrar AutoMapper si lo usaras:
        // services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Aquí también podrías registrar validadores (FluentValidation) si los usaras.

        return services; // Devuelve services para poder encadenar llamadas
    }
}