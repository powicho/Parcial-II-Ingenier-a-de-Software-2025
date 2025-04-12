// Este archivo va en: src/Interface/CleanArchitectureDemo.Api/Program.cs
using CleanArchitectureDemo.Application; // Para AddApplicationServices()
using CleanArchitectureDemo.Infrastructure; // Para AddInfrastructureServices()

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configurar Servicios (Inyección de Dependencias) ---

// Llama a los métodos de extensión que creamos en las otras capas
// para registrar todos sus servicios necesarios (MediatR, DbContext, Repositorios).
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration); // Pasamos la configuración por si la necesita (ej: ConnectionString)

// Agrega los servicios básicos para que funcionen los controladores de la API.
builder.Services.AddControllers();

// Agrega los servicios para generar la documentación de Swagger/OpenAPI.
// Esto nos dará una interfaz web para probar la API fácilmente.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CleanArchitectureDemo API",
        Version = "v1",
        Description = "Una API de ejemplo siguiendo los principios de Clean Architecture."
    });
});

var app = builder.Build(); // Construye la aplicación con los servicios configurados.

// --- 2. Configurar el Pipeline de Peticiones HTTP ---
// El orden aquí importa. Define cómo se procesa una petición entrante.

// Solo muestra la página de Swagger si estamos en modo Desarrollo (no en producción).
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita el middleware para generar el JSON de Swagger.
    app.UseSwaggerUI(c => // Habilita el middleware para mostrar la interfaz web de Swagger UI.
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitectureDemo API V1");
        // Descomenta la siguiente línea si quieres que Swagger sea la página de inicio al ejecutar:
        // c.RoutePrefix = string.Empty;
    });
}

// Redirige automáticamente las peticiones HTTP a HTTPS.
app.UseHttpsRedirection();

// (Aquí irían middlewares de Autenticación/Autorización si los tuvieras)
// app.UseAuthentication();
// app.UseAuthorization();

// Busca qué controlador y qué acción (método) debe manejar la petición entrante.
app.MapControllers();

// Inicia la aplicación y la pone a escuchar peticiones HTTP.
app.Run();