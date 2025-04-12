// Este archivo va en: src/Interface/CleanArchitectureDemo.Api/Program.cs
using CleanArchitectureDemo.Application; // Para AddApplicationServices()
using CleanArchitectureDemo.Infrastructure; // Para AddInfrastructureServices()

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configurar Servicios (Inyecci�n de Dependencias) ---

// Llama a los m�todos de extensi�n que creamos en las otras capas
// para registrar todos sus servicios necesarios (MediatR, DbContext, Repositorios).
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration); // Pasamos la configuraci�n por si la necesita (ej: ConnectionString)

// Agrega los servicios b�sicos para que funcionen los controladores de la API.
builder.Services.AddControllers();

// Agrega los servicios para generar la documentaci�n de Swagger/OpenAPI.
// Esto nos dar� una interfaz web para probar la API f�cilmente.
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

var app = builder.Build(); // Construye la aplicaci�n con los servicios configurados.

// --- 2. Configurar el Pipeline de Peticiones HTTP ---
// El orden aqu� importa. Define c�mo se procesa una petici�n entrante.

// Solo muestra la p�gina de Swagger si estamos en modo Desarrollo (no en producci�n).
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita el middleware para generar el JSON de Swagger.
    app.UseSwaggerUI(c => // Habilita el middleware para mostrar la interfaz web de Swagger UI.
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitectureDemo API V1");
        // Descomenta la siguiente l�nea si quieres que Swagger sea la p�gina de inicio al ejecutar:
        // c.RoutePrefix = string.Empty;
    });
}

// Redirige autom�ticamente las peticiones HTTP a HTTPS.
app.UseHttpsRedirection();

// (Aqu� ir�an middlewares de Autenticaci�n/Autorizaci�n si los tuvieras)
// app.UseAuthentication();
// app.UseAuthorization();

// Busca qu� controlador y qu� acci�n (m�todo) debe manejar la petici�n entrante.
app.MapControllers();

// Inicia la aplicaci�n y la pone a escuchar peticiones HTTP.
app.Run();