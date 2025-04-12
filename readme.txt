Nota: Este contenido es extenso. Cópialo y pégalo en un archivo llamado README.md en la raíz de tu repositorio Git (o en la carpeta principal de tu solución si no usas Git todavía).

# Clean Architecture Demo (.NET 8 API) - Guía Exhaustiva y Detallada

Este documento es una guía completa y detallada del proyecto API REST `CleanArchitectureDemo`. Su propósito es servir como documentación principal, manual de usuario y explicación profunda de la arquitectura y el código implementado. Está pensado para que cualquier miembro del equipo, independientemente de su nivel de experiencia previo con Arquitectura Limpia, pueda comprender el proyecto en su totalidad.

**Tabla de Contenidos**

1.  [Introducción y Contexto](#1-introducción-y-contexto)
    *   [1.1. Objetivo del Software](#11-objetivo-del-software)
    *   [1.2. El Desafío Arquitectónico](#12-el-desafío-arquitectónico)
    *   [1.3. La Solución: Arquitectura Limpia](#13-la-solución-arquitectura-limpia)
2.  [Principios de Arquitectura Limpia Aplicados](#2-principios-de-arquitectura-limpia-aplicados)
    *   [2.1. Las Capas Concétricas](#21-las-capas-concétricas)
    *   [2.2. La Regla de Dependencia (¡CRUCIAL!)](#22-la-regla-de-dependencia-crucial)
    *   [2.3. Beneficios Buscados](#23-beneficios-buscados)
3.  [Estructura del Proyecto (Explorador de Soluciones)](#3-estructura-del-proyecto-explorador-de-soluciones)
    *   [3.1. `CleanArchitectureDemo.Domain` (Core/Dominio ❤️)](#31-cleanarchitecturedemodomain-coredominio-️)
    *   [3.2. `CleanArchitectureDemo.Application` (Application/Servicios 🎼)](#32-cleanarchitecturedemoapplication-applicationservicios-)
    *   [3.3. `CleanArchitectureDemo.Infrastructure` (Infraestructura ⚙️)](#33-cleanarchitecturedemoinfrastructure-infraestructura-️)
    *   [3.4. `CleanArchitectureDemo.Api` (Interface/Presentación 🚪)](#34-cleanarchitecturedemoapi-interfacepresentación-)
4.  [Tecnologías Clave Utilizadas](#4-tecnologías-clave-utilizadas)
5.  [Instrucciones de Configuración y Ejecución](#5-instrucciones-de-configuración-y-ejecución)
    *   [5.1. Prerrequisitos](#51-prerrequisitos)
    *   [5.2. Obtener el Código](#52-obtener-el-código)
    *   [5.3. Restaurar Dependencias](#53-restaurar-dependencias)
    *   [5.4. ¡IMPORTANTE! Confiar en el Certificado HTTPS de Desarrollo](#54-importante-confiar-en-el-certificado-https-de-desarrollo)
    *   [5.5. Ejecutar la API](#55-ejecutar-la-api)
    *   [5.6. Acceder a Swagger UI](#56-acceder-to-swagger-ui)
6.  [Explorando la API con Swagger UI](#6-explorando-la-api-con-swagger-ui)
    *   [6.1. ¿Qué es Swagger UI?](#61-qué-es-swagger-ui)
    *   [6.2. Elementos Principales de la Interfaz](#62-elementos-principales-de-la-interfaz)
    *   [6.3. Flujo de Prueba CRUD Típico](#63-flujo-de-prueba-crud-típico)
7.  [Desglose Detallado del Código (¡MASIVO!)](#7-desglose-detallado-del-código-masivo)
    *   [7.1. Capa Domain (`CleanArchitectureDemo.Domain`)](#71-capa-domain-cleanarchitecturedemodomain)
        *   [7.1.1. `Entities/Category.cs`](#711-entitiescategorycs)
        *   [7.1.2. `Entities/Product.cs`](#712-entitiesproductcs)
    *   [7.2. Capa Application (`CleanArchitectureDemo.Application`)](#72-capa-application-cleanarchitecturedemoapplication)
        *   [7.2.1. `Interfaces/Persistence/IProductRepository.cs` (e `ICategoryRepository.cs`)](#721-interfacespersistenceiproductrepositorycs-e-icategoryrepositorycs)
        *   [7.2.2. `Features/.../Queries/ProductDto.cs` (y `CategoryDto.cs`)](#722-featuresqueriesproductdtocs-y-categorydtocs)
        *   [7.2.3. `Features/.../Commands/CreateProductCommand.cs`](#723-featurescommandscreateproductcommandcs)
        *   [7.2.4. `Features/.../Commands/CreateProductCommandHandler.cs`](#724-featurescommandscreateproductcommandhandlercs)
        *   [7.2.5. Otros Handlers (Concepto)](#725-otros-handlers-concepto)
        *   [7.2.6. `DependencyInjection.cs` (Application)](#726-dependencyinjectioncs-application)
    *   [7.3. Capa Infrastructure (`CleanArchitectureDemo.Infrastructure`)](#73-capa-infrastructure-cleanarchitecturedemoinfrastructure)
        *   [7.3.1. `Persistence/ApplicationDbContext.cs`](#731-persistenceapplicationdbcontextcs)
        *   [7.3.2. `Persistence/Repositories/ProductRepository.cs` (y `CategoryRepository.cs`)](#732-persistencerepositoriesproductrepositorycs-y-categoryrepositorycs)
        *   [7.3.3. `DependencyInjection.cs` (Infrastructure)](#733-dependencyinjectioncs-infrastructure)
    *   [7.4. Capa Api (`CleanArchitectureDemo.Api`)](#74-capa-api-cleanarchitecturedemoapi)
        *   [7.4.1. `Program.cs`](#741-programcs)
        *   [7.4.2. `Controllers/ProductsController.cs` (y `CategoriesController.cs`)](#742-controllersproductscontrollercs-y-categoriescontrollercs)
        *   [7.4.3. Archivos Auxiliares (`appsettings.json`, `launchSettings.json`)](#743-archivos-auxiliares-appsettingsjson-launchsettingsjson)
8.  [Decisiones Clave de Diseño (Justificación)](#8-decisiones-clave-de-diseño-justificación)
9.  [Glosario de Conceptos Clave ("¿QUÉ ES?")](#9-glosario-de-conceptos-clave-qué-es)
10. [Conclusión](#10-conclusión)

---

## 1. Introducción y Contexto

### 1.1. Objetivo del Software

Este proyecto, `CleanArchitectureDemo`, implementa una **API REST** básica pero funcional para gestionar un catálogo simple de Productos y sus correspondientes Categorías. Permite realizar las operaciones **CRUD** (Crear, Leer, Actualizar, Borrar) sobre estos dos recursos. Sirve como un ejemplo práctico y didáctico, especialmente útil para entender cómo aplicar patrones de diseño de software modernos en un contexto de .NET 8.

### 1.2. El Desafío Arquitectónico

Aunque la funcionalidad requerida (CRUD) es común, un desafío frecuente en el desarrollo de software es cómo estructurar el código de manera que sea:

*   **Mantenible:** Fácil de entender, modificar y corregir a lo largo del tiempo.
*   **Escalable:** Capaz de crecer en funcionalidad sin volverse un caos inmanejable ("código espagueti").
*   **Testeable:** Que las partes importantes se puedan probar de forma fiable y aislada.
*   **Flexible:** Adaptible a cambios en la tecnología (UI, base de datos, servicios externos) sin requerir una reescritura masiva.

### 1.3. La Solución: Arquitectura Limpia

Para abordar estos desafíos, este proyecto adopta los principios de la **Arquitectura Limpia** (Clean Architecture), popularizada por Robert C. Martin ("Uncle Bob"). No es un framework estricto, sino un conjunto de guías de diseño que enfatizan la **separación de responsabilidades** y el **control de las dependencias** entre las diferentes partes del sistema.

---

## 2. Principios de Arquitectura Limpia Aplicados

### 2.1. Las Capas Concétricas

Visualizamos la arquitectura como círculos concéntricos, donde el interior es lo más abstracto y estable, y el exterior contiene los detalles concretos y volátiles:

```mermaid
graph TD
    A(Frameworks & Drivers\n(UI, DB, Web Server)\nApi, Infrastructure);
    B(Interface Adapters\n(Controllers, Presenters,\nGateways/Repositories));
    C(Application Business Rules\n(Use Cases)\nApplication);
    D(Enterprise Business Rules\n(Entities)\nDomain);

    subgraph DetallesTecnicos [DETALLES TÉCNICOS]
        A
        B
    end

    subgraph ReglasAplicacion [REGLAS DE APLICACIÓN]
        C
    end

     subgraph ReglasNegocioNucleo [REGLAS DE NEGOCIO NÚCLEO]
        D
    end

    A -- dependencia --> C;
    B -- dependencia --> C;
    C -- dependencia --> D;

    style ReglasNegocioNucleo fill:#FFDAB9,stroke:#CD853F,stroke-width:2px
    style ReglasAplicacion fill:#E6E6FA,stroke:#9370DB,stroke-width:2px
    style DetallesTecnicos fill:#F0FFF0,stroke:#3CB371,stroke-width:2px


Domain (Núcleo): Contiene las Entidades y las reglas de negocio más fundamentales, independientes de cualquier aplicación específica.

Application (Casos de Uso): Orquesta los flujos de la aplicación, contiene la lógica específica y define las interfaces para las capas externas.

Interface Adapters (Adaptadores): Convierten datos entre los casos de uso y los elementos externos (UI, Base de Datos). Incluye Controladores, Presentadores, y las implementaciones de los Repositorios.

Frameworks & Drivers (Exterior): Los detalles más concretos: la UI (Swagger en nuestro caso), la Base de Datos (InMemory), el Framework Web (ASP.NET Core), etc.

2.2. La Regla de Dependencia (¡CRUCIAL!)

La regla más importante: Las dependencias del código fuente SÓLO pueden apuntar hacia adentro.

Application puede depender de Domain.

Infrastructure puede depender de Application y Domain.

Api puede depender de Application.

NUNCA Domain debe depender de Application.

NUNCA Application debe depender de Infrastructure o Api.

Esto se logra mediante el Principio de Inversión de Dependencias (DIP): Las capas internas definen interfaces (contratos) que las capas externas implementan. Luego, usamos Inyección de Dependencias (DI) para "conectar" la implementación correcta en tiempo de ejecución.

2.3. Beneficios Buscados

Independencia del Framework: El núcleo (Domain, Application) no depende de ASP.NET Core.

Testeabilidad: Domain y Application se pueden probar unitariamente sin dependencias externas.

Independencia de la UI: La capa Api (y Swagger UI) podría reemplazarse sin tocar Application o Domain.

Independencia de la Base de Datos: Application solo depende de IRepository. La implementación en Infrastructure (con EF Core InMemory) se puede cambiar fácilmente.

3. Estructura del Proyecto (Explorador de Soluciones)

La solución CleanArchitectureDemo.sln está organizada reflejando las capas:

CleanArchitectureDemo/
└── src/
    ├── Core/
    │   └── CleanArchitectureDemo.Domain/      (Proyecto .NET Class Library) ❤️
    │       └── Entities/
    │           ├── Category.cs
    │           └── Product.cs
    ├── Application/
    │   └── CleanArchitectureDemo.Application/ (Proyecto .NET Class Library) 🎼
    │       ├── Features/
    │       │   ├── Categories/
    │       │   │   ├── Commands/ (+ Queries)
    │       │   │   └── ... (Handlers, DTOs)
    │       │   └── Products/
    │       │       ├── Commands/ (+ Queries)
    │       │       └── ... (Handlers, DTOs)
    │       ├── Interfaces/
    │       │   └── Persistence/
    │       │       ├── ICategoryRepository.cs
    │       │       └── IProductRepository.cs
    │       └── DependencyInjection.cs
    ├── Infrastructure/
    │   └── CleanArchitectureDemo.Infrastructure/ (Proyecto .NET Class Library) ⚙️
    │       ├── Persistence/
    │       │   ├── ApplicationDbContext.cs
    │       │   └── Repositories/
    │       │       ├── CategoryRepository.cs
    │       │       └── ProductRepository.cs
    │       └── DependencyInjection.cs
    └── Interface/
        └── CleanArchitectureDemo.Api/          (Proyecto ASP.NET Core Web API) 🚪
            ├── Controllers/
            │   ├── CategoriesController.cs
            │   └── ProductsController.cs
            ├── Program.cs
            ├── appsettings.json
            └── ... (Otros archivos de API)
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
IGNORE_WHEN_COPYING_END
3.1. CleanArchitectureDemo.Domain (Core/Dominio ❤️)

Propósito: El corazón. Define las entidades y reglas de negocio centrales.

Responsabilidades: Definir qué es un Product y una Category, incluyendo sus propiedades y validaciones inherentes (ej: un precio no puede ser negativo).

Dependencias: NINGUNA con otros proyectos de la solución.

Contenido Clave: Clases Entidad (Product, Category).

3.2. CleanArchitectureDemo.Application (Application/Servicios 🎼)

Propósito: El orquestador. Define los casos de uso y la lógica de aplicación.

Responsabilidades: Definir Comandos (ej: CreateProductCommand), Queries (ej: GetAllCategoriesQuery), sus Handlers correspondientes (la lógica para ejecutarlos), los DTOs para transferencia de datos, y las Interfaces para dependencias externas (ICategoryRepository, IProductRepository).

Dependencias: SOLO de Domain.

Contenido Clave: Carpetas Features, Interfaces, archivo DependencyInjection para registrar Handlers (MediatR).

3.3. CleanArchitectureDemo.Infrastructure (Infraestructura ⚙️)

Propósito: La caja de herramientas. Provee las implementaciones concretas de las interfaces definidas en Application.

Responsabilidades: Implementar los repositorios (CategoryRepository, ProductRepository) usando una tecnología específica (EF Core InMemory), configurar el DbContext (ApplicationDbContext). Contendría otros detalles como clientes de servicios externos (email, etc.) si los hubiera.

Dependencias: De Application (para implementar interfaces) y Domain (para conocer las entidades).

Contenido Clave: ApplicationDbContext, clases Repository, archivo DependencyInjection para registrar DbContext y repositorios.

3.4. CleanArchitectureDemo.Api (Interface/Presentación 🚪)

Propósito: La puerta de entrada. Expone la funcionalidad como una API REST.

Responsabilidades: Definir los Controllers (CategoriesController, ProductsController) que reciben peticiones HTTP. Mapear estas peticiones a Comandos/Queries de Application. Usar MediatR (_mediator.Send) para invocar la lógica de Application. Formatear las respuestas de Application en respuestas HTTP (Ok, Created, NotFound, etc.). Configurar la aplicación web (Program.cs), incluyendo DI y Swagger.

Dependencias: De Application (para enviar Comandos/Queries) y de Infrastructure (SOLO para la configuración de DI en Program.cs).

Contenido Clave: Clases Controller, archivo Program.cs.

4. Tecnologías Clave Utilizadas

.NET 8: La última versión LTS (Long-Term Support) del framework de desarrollo de Microsoft.

ASP.NET Core: El framework específico dentro de .NET para construir aplicaciones web y APIs modernas, rápidas y multiplataforma.

Entity Framework Core (EF Core) 8: El ORM (Object-Relational Mapper) de Microsoft. Permite interactuar con bases de datos usando objetos .NET en lugar de SQL directo.

Proveedor InMemory: Usado en este proyecto para una base de datos temporal que vive en memoria (ideal para demos y pruebas rápidas, pero los datos se borran al detener la app).

Proveedor Relational: Paquete adicional necesario para ciertas configuraciones como HasColumnType.

MediatR: Una librería popular para implementar los patrones Mediator y CQRS en .NET de forma simple. Ayuda a desacoplar componentes (Controllers de Handlers).

Swagger (OpenAPI / Swashbuckle):

OpenAPI: El estándar para describir APIs REST.

Swashbuckle.AspNetCore: La librería que genera automáticamente la documentación OpenAPI (swagger.json) a partir de los controladores ASP.NET Core y provee la interfaz web Swagger UI para visualizarla y probarla.

Inyección de Dependencias (DI) Integrada: Se utiliza el contenedor de DI incorporado en ASP.NET Core para gestionar la creación y el ciclo de vida de los servicios (DbContext, Repositorios, MediatR, etc.).

5. Instrucciones de Configuración y Ejecución

Sigue estos pasos para ejecutar la API en tu máquina local:

5.1. Prerrequisitos

SDK de .NET 8: Asegúrate de tener instalado el Software Development Kit de .NET 8 (Descarga desde Microsoft).

Un Editor de Código o IDE:

Visual Studio 2022 (Recomendado, con la carga de trabajo "Desarrollo de ASP.NET y web" instalada).

Visual Studio Code con la extensión C# Dev Kit.

JetBrains Rider.

(Opcional) Git: Para clonar el repositorio si el código fuente está en uno.

5.2. Obtener el Código

Si está en un repositorio Git, clónalo: git clone <URL_DEL_REPOSITORIO> y navega a la carpeta creada.

Si tienes los archivos en una carpeta, asegúrate de estar en la carpeta raíz que contiene el archivo .sln.

5.3. Restaurar Dependencias

Abre una terminal o línea de comandos en la carpeta raíz de la solución (donde está CleanArchitectureDemo.sln) y ejecuta:

dotnet restore
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
Bash
IGNORE_WHEN_COPYING_END

Esto descargará todas las librerías externas necesarias (NuGet packages) definidas en los proyectos.

5.4. ¡IMPORTANTE! Confiar en el Certificado HTTPS de Desarrollo

ASP.NET Core usa HTTPS por defecto para el desarrollo local, lo cual requiere un certificado "autofirmado". Tu máquina necesita confiar en este certificado para evitar errores de conexión en el navegador.

ABRE una terminal (CMD, PowerShell) COMO ADMINISTRADOR.

Busca "cmd" o "powershell" en Inicio -> Clic derecho -> Ejecutar como administrador.

EJECUTA el siguiente comando:

dotnet dev-certs https --trust
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
Bash
IGNORE_WHEN_COPYING_END

ACEPTA el diálogo de seguridad que aparecerá haciendo clic en "Sí". Si no aparece o no haces clic en "Sí", HTTPS no funcionará correctamente.

Puedes cerrar la terminal de administrador.

5.5. Ejecutar la API

Tienes dos opciones principales:

Desde Visual Studio:

Abre el archivo CleanArchitectureDemo.sln.

Asegúrate de que el proyecto CleanArchitectureDemo.Api esté seleccionado como proyecto de inicio (debería estar en negrita en el Explorador de Soluciones. Si no, clic derecho -> Establecer como proyecto de inicio).

Presiona F5 o haz clic en el botón de ejecución verde ▶️ con el nombre CleanArchitectureDemo.Api.

Desde la Línea de Comandos:

Navega en tu terminal a la carpeta del proyecto API: cd src/Interface/CleanArchitectureDemo.Api (o la ruta correcta).

Ejecuta:

dotnet run
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
Bash
IGNORE_WHEN_COPYING_END

La consola mostrará mensajes indicando que la aplicación se inició y en qué URLs está escuchando (normalmente algo como https://localhost:7089 y http://localhost:5032).

5.6. Acceder a Swagger UI

Si ejecutaste desde Visual Studio o si la configuración por defecto de launchSettings.json está activa, tu navegador web debería abrirse automáticamente en la página de Swagger UI.

Si no se abre, o si ejecutaste desde la línea de comandos, abre manualmente tu navegador y ve a la URL HTTPS indicada en la consola, añadiendo /swagger al final. Ejemplo:

https://localhost:7089/swagger

(Reemplaza 7089 por el puerto HTTPS que muestre tu consola si es diferente).

6. Explorando la API con Swagger UI
6.1. ¿Qué es Swagger UI?

Es una interfaz web interactiva generada automáticamente que sirve como:

Documentación Viva: Describe todos los endpoints (URLs) disponibles en tu API, qué métodos HTTP aceptan (GET, POST, PUT, DELETE), qué parámetros necesitan, y qué respuestas pueden dar.

Herramienta de Pruebas: Te permite enviar peticiones reales a tu API directamente desde el navegador, sin necesidad de usar herramientas como Postman o escribir código cliente.

6.2. Elementos Principales de la Interfaz

Título y Descripción: Información general de la API.

Secciones por Controlador: Agrupaciones de operaciones (ej: Categories, Products).

Barras de Operación: Cada barra representa un endpoint específico (ej: POST /api/categories, GET /api/products/{id}). El color indica el método HTTP.

"Try it out": Botón para habilitar la prueba de un endpoint.

"Parameters": Campos para introducir valores que van en la URL (como {id}) o query string.

"Request body": Área (para POST/PUT) donde escribes el cuerpo de la petición, usualmente en formato JSON. Se muestra un ejemplo.

"Execute": Botón para enviar la petición real a tu API en ejecución.

"Responses": Muestra el resultado de la ejecución:

Code: El código de estado HTTP (200, 201, 204, 404, etc.).

Response body: El cuerpo de la respuesta devuelta por la API (si lo hay), usualmente JSON.

Response headers: Cabeceras HTTP de la respuesta.

Curl: Un comando curl equivalente que podrías usar en una terminal para hacer la misma petición.

6.3. Flujo de Prueba CRUD Típico

Crear (POST): Usa POST /api/categories (o products). Rellena el Request body con los datos necesarios (ej: {"name": "Test"}). Ejecuta. Obtén la respuesta 201 Created. COPIA EL ID del Response body.

Leer Específico (GET por ID): Usa GET /api/categories/{id}. Pega el ID copiado en el parámetro id. Ejecuta. Deberías ver la respuesta 200 OK con los datos del elemento creado.

Leer Todos (GET): Usa GET /api/categories. Ejecuta. Deberías ver el elemento creado en la lista de respuesta (200 OK).

Actualizar (PUT): Usa PUT /api/categories/{id}. Pega el ID en el parámetro id. Modifica el Request body con los nuevos datos (ej: {"name": "Test Updated"}). Ejecuta. Deberías obtener 204 No Content. Verifica volviendo a hacer el GET por ID.

Borrar (DELETE): Usa DELETE /api/categories/{id}. Pega el ID en el parámetro id. Ejecuta. Deberías obtener 204 No Content.

Verificar Borrado: Intenta hacer GET /api/categories/{id} con el mismo ID borrado. Deberías obtener 404 Not Found.

7. Desglose Detallado del Código (¡MASIVO!)

(Esta sección reutiliza y consolida las explicaciones detalladas previas)

7.1. Capa Domain (CleanArchitectureDemo.Domain)

Propósito: Núcleo del negocio, entidades y reglas fundamentales. Independiente.

Ubicación: src/Core/CleanArchitectureDemo.Domain/

7.1.1. Entities/Category.cs

Define qué es una categoría.

namespace CleanArchitectureDemo.Domain.Entities; // Agrupación lógica

public class Category // Plano para objetos Categoría
{
    // ID único global, legible pero inmutable desde fuera
    public Guid Id { get; private set; }
    // Nombre, legible pero inmutable desde fuera (se cambia vía Update)
    public string Name { get; private set; } = string.Empty;

    // Constructor privado: Fuerza creación vía 'Create', necesario para EF Core
    private Category() { }

    // Fábrica estática: Única forma pública de crear categorías
    public static Category Create(string name)
    {
        // Regla de negocio: El nombre es obligatorio
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));
        // Devuelve una instancia válida con ID nuevo
        return new Category { Id = Guid.NewGuid(), Name = name };
    }

    // Método para modificar una categoría existente
    public void Update(string name)
    {
        // Revalida la regla de negocio
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));
        // Actualiza el estado interno
        Name = name;
    }
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.1.2. Entities/Product.cs

Define qué es un producto.

namespace CleanArchitectureDemo.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    // Descripción opcional (nullable string)
    public string? Description { get; private set; }
    // Precio (tipo decimal para precisión monetaria)
    public decimal Price { get; private set; }
    // ID de la categoría a la que pertenece (relación)
    public Guid CategoryId { get; private set; }

    private Product() { } // Constructor privado

    // Fábrica estática con validaciones
    public static Product Create(string name, string? description, decimal price, Guid categoryId)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));
        if (price <= 0) // Precio debe ser positivo
            throw new ArgumentException("Product price must be positive.", nameof(price));
        if (categoryId == Guid.Empty) // Debe tener categoría
            throw new ArgumentException("Category ID must be provided.", nameof(categoryId));

        return new Product // Crea instancia válida
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            CategoryId = categoryId
        };
    }

    // Método para actualizar, con las mismas validaciones
    public void Update(string name, string? description, decimal price, Guid categoryId)
    {
         if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));
        if (price <= 0)
            throw new ArgumentException("Product price must be positive.", nameof(price));
         if (categoryId == Guid.Empty)
            throw new ArgumentException("Category ID must be provided.", nameof(categoryId));

        // Actualiza estado interno
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.2. Capa Application (CleanArchitectureDemo.Application)

Propósito: Orquestar casos de uso, lógica de aplicación, definir interfaces.

Ubicación: src/Application/CleanArchitectureDemo.Application/

7.2.1. Interfaces/Persistence/IProductRepository.cs (e ICategoryRepository.cs)

Define el contrato para acceder a datos de productos.

// Using necesarios para tipos (Guid, IEnumerable, Task, CancellationToken) y entidades (Product)
using CleanArchitectureDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Application.Interfaces.Persistence;

// Contrato para operaciones CRUD de Productos
public interface IProductRepository
{
    // Devuelve un Producto o null si no existe (operación async)
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    // Devuelve todos los Productos (operación async)
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    // Marca un Producto para añadir (operación async)
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    // Marca un Producto para actualizar (sync - EF Core rastrea)
    void Update(Product product);
    // Marca un Producto para borrar (sync - EF Core rastrea)
    void Delete(Product product);
    // Guarda todos los cambios pendientes en la BD (Unidad de Trabajo, op async)
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END

(ICategoryRepository es idéntico pero opera sobre Category).

7.2.2. Features/.../Queries/ProductDto.cs (y CategoryDto.cs)

Objeto para transferir datos de producto hacia afuera.

namespace CleanArchitectureDemo.Application.Features.Categories.Queries; // (O Products)

// DTO: Simple contenedor de datos, sin lógica de negocio.
public class CategoryDto // (O ProductDto)
{
    // Propiedades públicas con get/set para fácil serialización/deserialización (ej: a JSON)
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    // ProductDto tendría más propiedades: Description, Price, CategoryId
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.2.3. Features/.../Commands/CreateProductCommand.cs

Representa la intención y datos para crear un producto. Usa MediatR.

using MediatR; // Necesario para IRequest<T>
using CleanArchitectureDemo.Application.Features.Products.Queries; // Namespace del DTO
using System;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;

// 'record': Inmutable, para datos.
// Parámetros: Los datos necesarios.
// ': IRequest<ProductDto>': Declara que es un comando MediatR que espera una respuesta ProductDto.
public record CreateProductCommand(string Name, string? Description, decimal Price, Guid CategoryId) : IRequest<ProductDto>;
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END

(Otras clases Command/Query son similares, implementando IRequest<T> con el tipo de respuesta esperado: IRequest<bool>, IRequest<IEnumerable<ProductDto>>, IRequest<ProductDto?>).

7.2.4. Features/.../Commands/CreateProductCommandHandler.cs

Contiene la lógica para procesar CreateProductCommand. Usa MediatR.

// Usings necesarios (MediatR, Interfaz Repo, Entidad, DTO, Async)
using MediatR;
using CleanArchitectureDemo.Application.Interfaces.Persistence;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Application.Features.Products.Queries;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;

// Implementa IRequestHandler<TCommand, TResponse> de MediatR
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    // Dependencia de la INTERFAZ del repositorio (Inyección de Dependencias)
    private readonly IProductRepository _productRepository;

    // Constructor para DI: Recibe la implementación concreta del repositorio
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    // Método Handle: Contiene la lógica del caso de uso
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Llama a la lógica de Domain para crear la entidad (con validaciones)
        var product = Product.Create(request.Name, request.Description, request.Price, request.CategoryId);

        // 2. Usa la interfaz del repositorio para añadir (sin saber de EF Core)
        await _productRepository.AddAsync(product, cancellationToken);

        // 3. Usa la interfaz del repositorio para guardar cambios (Unidad de Trabajo)
        await _productRepository.SaveChangesAsync(cancellationToken);

        // 4. Mapea la entidad guardada a un DTO para devolverla
        return new ProductDto { /* mapear propiedades */ };
    }
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.2.5. Otros Handlers (Concepto)

Query Handlers (GetAll...Handler, GetById...Handler): Usualmente solo llaman a métodos Get...Async del repositorio y mapean los resultados (Entidades) a DTOs. No llaman a SaveChangesAsync.

Update/Delete Handlers: Usualmente llaman a GetByIdAsync para encontrar la entidad, llaman al método Update o Delete de la entidad (si aplica), llaman a Update o Delete del repositorio, y finalmente llaman a SaveChangesAsync. Suelen devolver bool (IRequest<bool>) para indicar éxito/fallo (o pueden lanzar excepciones si no se encuentra).

7.2.6. DependencyInjection.cs (Application)

Registra los servicios de esta capa, principalmente MediatR.

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitectureDemo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Configura MediatR para que escanee este Assembly (proyecto)
        // y encuentre y registre automáticamente todos los Handlers (IRequestHandler).
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Registraríamos otros servicios de Application aquí (AutoMapper, FluentValidation, etc.)
        return services;
    }
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.3. Capa Infrastructure (CleanArchitectureDemo.Infrastructure)

Propósito: Implementaciones concretas y detalles técnicos (BD).

Ubicación: src/Infrastructure/CleanArchitectureDemo.Infrastructure/

7.3.1. Persistence/ApplicationDbContext.cs

Contexto de Entity Framework Core.

using Microsoft.EntityFrameworkCore;
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext // Hereda de EF Core
{
    // Constructor para recibir opciones (configuradas por DI)
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // DbSet para cada entidad = Tabla en la BD
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    // Configuración del modelo BD usando Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id); // Clave Primaria
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // No nulo, max 100
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // Tipo decimal BD
        });
        modelBuilder.Entity<Category>(entity => { /* ... similar ... */ });

        base.OnModelCreating(modelBuilder);
    }
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.3.2. Persistence/Repositories/ProductRepository.cs (y CategoryRepository.cs)

Implementación concreta de la interfaz de repositorio usando EF Core.

// Usings (EF Core, Interfaz de App, Entidad de Domain, etc.)
using Microsoft.EntityFrameworkCore;
using CleanArchitectureDemo.Application.Interfaces.Persistence;
using CleanArchitectureDemo.Domain.Entities;
// ...otros usings...

namespace CleanArchitectureDemo.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository // Implementa la interfaz
{
    // Dependencia del DbContext (Inyección de Dependencias)
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) // Recibe DbContext vía DI
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Implementaciones usando _context (EF Core)
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // Usa los métodos del DbContext/DbSet
        return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        // Marca la entidad como añadida en el tracker de EF
        await _context.Products.AddAsync(product, cancellationToken);
    }

    public void Update(Product product)
    {
        // Marca la entidad como modificada en el tracker de EF
        _context.Entry(product).State = EntityState.Modified;
    }

     public void Delete(Product product)
    {
        // Marca la entidad como eliminada en el tracker de EF
         _context.Products.Remove(product);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       // Delega al SaveChangesAsync del DbContext para ejecutar la transacción en la BD
       return await _context.SaveChangesAsync(cancellationToken);
    }
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.3.3. DependencyInjection.cs (Infrastructure)

Registra los servicios de esta capa (DbContext y Repositorios concretos).

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration; // Para IConfiguration
using Microsoft.EntityFrameworkCore; // Para AddDbContext y UseInMemoryDatabase
using CleanArchitectureDemo.Infrastructure.Persistence; // Para ApplicationDbContext
using CleanArchitectureDemo.Application.Interfaces.Persistence; // Para las interfaces IRepository
using CleanArchitectureDemo.Infrastructure.Persistence.Repositories; // Para las clases Repository

namespace CleanArchitectureDemo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration) // Puede usar la config general
    {
        // Configura EF Core: Registra el DbContext y le dice que use la BD en Memoria.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CleanArchDemoDb"));
        // Aquí cambiarías a UseSqlServer, UseNpgsql, etc., leyendo `configuration` para el connection string.

        // Registro de la Inyección de Dependencias para los Repositorios:
        // Cuando Application pida IProductRepository, DI le dará ProductRepository.
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // Registraríamos otros servicios de Infra (Email, etc.) aquí.
        return services;
    }
}
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.4. Capa Api (CleanArchitectureDemo.Api)

Propósito: Exponer la API vía HTTP, recibir peticiones, enviar comandos/queries, devolver respuestas.

Ubicación: src/Interface/CleanArchitectureDemo.Api/

7.4.1. Program.cs

Punto de entrada y configuración central de la API ASP.NET Core.

using CleanArchitectureDemo.Application; // Para AddApplicationServices
using CleanArchitectureDemo.Infrastructure; // Para AddInfrastructureServices
// using's de ASP.NET Core

var builder = WebApplication.CreateBuilder(args); // Inicia configuración

// --- REGISTRO DE SERVICIOS (Inyección de Dependencias) ---
builder.Services.AddApplicationServices(); // Registra MediatR y sus Handlers
builder.Services.AddInfrastructureServices(builder.Configuration); // Registra DbContext y Repositories
builder.Services.AddControllers(); // Servicios para Controladores MVC/API
// Servicios para Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { /* configura título, etc. */ });

var app = builder.Build(); // Construye la app

// --- CONFIGURACIÓN DEL PIPELINE HTTP (Middlewares) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genera swagger.json
    app.UseSwaggerUI(c => { /* sirve la UI web de Swagger */ }); // Sirve la página
}
app.UseHttpsRedirection(); // Fuerza HTTPS
// app.UseAuthentication(); // (Si hubiera login)
// app.UseAuthorization(); // (Si hubiera permisos)
app.MapControllers(); // Dirige las peticiones a los métodos de los Controllers

app.Run(); // Inicia el servidor y espera peticiones
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END
7.4.2. Controllers/ProductsController.cs (y CategoriesController.cs)

Maneja las peticiones HTTP para productos.

using MediatR; // Para ISender
using Microsoft.AspNetCore.Mvc; // Para ControllerBase y atributos [ApiController], [Route], [HttpGet]...
// using's para los Commands, Queries y DTOs de Application que usa
using CleanArchitectureDemo.Application.Features.Products...;
// using's estándar (Guid, Task, etc.)

namespace CleanArchitectureDemo.Api.Controllers;

[ApiController] // Marca como controlador API
[Route("api/[controller]")] // Ruta base = /api/products
public class ProductsController : ControllerBase
{
    private readonly ISender _mediator; // Dependencia de MediatR (inyectada)

    public ProductsController(ISender mediator) // Recibe ISender vía DI
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // --- Métodos de Acción (Endpoints) ---

    [HttpGet] // GET /api/products
    // Documentación Swagger de la respuesta
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var query = new GetAllProductsQuery(); // Crear Query
        var result = await _mediator.Send(query, cancellationToken); // Enviar vía MediatR
        return Ok(result); // Devolver Respuesta HTTP 200 OK con los datos
    }

    [HttpGet("{id:guid}")] // GET /api/products/GUID
    // ... otros [ProducesResponseType] ...
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result is not null ? Ok(result) : NotFound(); // Devuelve 200 o 404
    }

    [HttpPost] // POST /api/products
    // ... [ProducesResponseType] ...
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken ct)
    // [FromBody]: Lee el JSON del cuerpo de la petición y lo convierte en CreateProductCommand
    {
        var result = await _mediator.Send(command, ct);
        // Devuelve 201 Created con la URL al nuevo recurso y el propio recurso
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")] // PUT /api/products/GUID
     // ... [ProducesResponseType] ...
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest req, CancellationToken ct)
    {
        var command = new UpdateProductCommand(id, req.Name, req.Description, req.Price, req.CategoryId);
        var success = await _mediator.Send(command, ct);
        return success ? NoContent() : NotFound(); // Devuelve 204 o 404
    }

    [HttpDelete("{id:guid}")] // DELETE /api/products/GUID
    // ... [ProducesResponseType] ...
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        var success = await _mediator.Send(command, cancellationToken);
        return success ? NoContent() : NotFound(); // Devuelve 204 o 404
    }
}

// Record auxiliar para el cuerpo del PUT, puede diferir ligeramente del Command si es necesario
public record UpdateProductRequest(string Name, string? Description, decimal Price, Guid CategoryId);
IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
C#
IGNORE_WHEN_COPYING_END

(CategoriesController es muy similar pero opera sobre categorías y sus respectivos Commands/Queries).

7.4.3. Archivos Auxiliares (appsettings.json, launchSettings.json)

appsettings.json: Contiene configuración de la aplicación (ej: niveles de logging). Aquí añadirías la ConnectionStrings si usaras una base de datos real.

launchSettings.json: Contiene perfiles de cómo Visual Studio debe lanzar la aplicación para depurar (ej: las URLs http://localhost:5032 y https://localhost:7089, si debe abrir el navegador y a qué URL, variables de entorno como ASPNETCORE_ENVIRONMENT=Development).

8. Decisiones Clave de Diseño (Justificación)

Arquitectura Limpia: Elegida por sus beneficios en mantenibilidad, testeabilidad, escalabilidad y flexibilidad a largo plazo, separando claramente las responsabilidades.

.NET 8 / ASP.NET Core: Framework moderno, de alto rendimiento, multiplataforma y con excelente soporte para desarrollo de APIs y DI integrada.

EF Core con proveedor InMemory: Permite un inicio rápido y demostraciones sin necesidad de configurar una base de datos real. Se reconoce que los datos son volátiles. El diseño permite cambiar fácilmente a otro proveedor (SQL Server, PostgreSQL) modificando principalmente Infrastructure/DependencyInjection.cs y appsettings.json.

MediatR: Facilita la implementación del patrón CQRS y desacopla los Controllers (que solo envían mensajes) de los Handlers (que contienen la lógica). Reduce las dependencias directas y mejora la cohesión de los casos de uso. Permite futuras extensiones con comportamientos de Pipeline.

Patrón Repositorio: Abstrae el acceso a datos (IProductRepository), aislando la capa Application de los detalles de implementación de Infrastructure (EF Core). Permite cambiar la fuente de datos más fácilmente.

Unidad de Trabajo (implícita con DbContext): El DbContext de EF Core actúa como una UoW, y SaveChangesAsync confirma la transacción, asegurando la atomicidad de las operaciones de escritura.

GUIDs para IDs: Se eligieron por su unicidad global, evitando problemas de colisión si el sistema escalara o se distribuyera, y eliminando la necesidad de que la base de datos genere IDs secuenciales.

DTOs: Se usan para transferir datos hacia/desde la capa Api, evitando exponer las entidades de Domain directamente. Esto mejora el desacoplamiento y permite adaptar los datos al contrato de la API.

record Types (para Comandos, Queries, DTOs): Proveen una sintaxis concisa y son ideales para tipos inmutables orientados a datos, reduciendo el código boilerplate.

Inyección de Dependencias Nativa: Se aprovecha el robusto contenedor de DI integrado en ASP.NET Core, configurado en Program.cs y los archivos DependencyInjection.cs de cada capa.

Swagger/OpenAPI: Integrado para proporcionar documentación interactiva y facilitar las pruebas manuales durante el desarrollo, generado automáticamente a partir del código.

9. Glosario de Conceptos Clave ("¿QUÉ ES?")

API (Application Programming Interface): Conjunto de reglas/definiciones para que programas se comuniquen. Nuestra API expone operaciones CRUD.

REST (Representational State Transfer): Estilo arquitectónico para APIs web (usa HTTP, URLs para recursos, JSON).

HTTP (Hypertext Transfer Protocol): Protocolo para peticiones/respuestas web.

Métodos HTTP: Verbos de la petición (GET, POST, PUT, DELETE).

Códigos de Estado HTTP: Números indicando el resultado (200 OK, 201 Created, 204 No Content, 400 Bad Request, 404 Not Found).

JSON (JavaScript Object Notation): Formato estándar de intercambio de datos para APIs REST.

Swagger / OpenAPI: Estándar (OpenAPI) y herramientas (Swagger) para describir y documentar/probar APIs REST. Swagger UI es la página interactiva que usamos.

GUID (Globally Unique Identifier): Identificador único de 128 bits (tipo System.Guid).

Namespace: Agrupación lógica de código en C#.

Using Directive (using ...;): Abreviatura para no escribir nombres completos de tipos.

Clase / Objeto: Plantilla / Instancia de la plantilla.

Propiedad: Dato de una clase (con get/set). private set limita escritura.

Método: Bloque de código que realiza una acción en una clase.

Constructor: Método especial para inicializar un objeto.

Static: Miembro que pertenece a la clase, no a la instancia.

Void: Método que no devuelve valor.

Record: Tipo especial en C# para datos inmutables.

Interface: Contrato que define miembros sin implementación.

Herencia / Implementación (:): Una clase obtiene/implementa miembros de otra clase/interfaz.

Async / Await: Palabras clave para programación asíncrona (no bloqueante).

Task / Task<T>: Representan operaciones asíncronas.

IActionResult: Interfaz en ASP.NET Core para devolver resultados HTTP variados.

Atributos ([...]): Metadatos para decorar código ([ApiController], [HttpGet]...).

Model Binding: Conversión automática de datos HTTP a parámetros de métodos de Controller.

Inyección de Dependencias (DI): Patrón para suministrar dependencias a una clase vía constructor.

Contenedor de DI (builder.Services): Administrador de servicios y sus dependencias.

Tiempo de Vida del Servicio (AddScoped, etc.): Cuándo se crean/destruyen las instancias de servicios.

MediatR: Librería para patrones Mediator y CQRS.

CQRS: Separar operaciones de escritura (Commands) y lectura (Queries).

EF Core: ORM de Microsoft para bases de datos.

ORM: Herramienta que mapea objetos a bases de datos relacionales.

DbContext: Sesión con la base de datos en EF Core.

DbSet: Representación de una tabla en el DbContext.

LINQ: Lenguaje de consulta integrado en C#.

Middleware: Componentes que procesan peticiones HTTP en cadena en ASP.NET Core.

Kestrel: Servidor web integrado en ASP.NET Core.

10. Conclusión

Este proyecto sirve como una implementación concreta y funcional de los principios de Arquitectura Limpia aplicados a una API REST con .NET 8. Demuestra cómo la separación de capas, el control estricto de dependencias, el uso de interfaces, la inyección de dependencias y patrones como CQRS/MediatR y Repositorio contribuyen a crear una base de código más robusta, mantenible, testeable y flexible. Swagger UI actúa como una herramienta invaluable para documentar y probar la funcionalidad expuesta por la capa de API.


IGNORE_WHEN_COPYING_START
content_copy
download
Use code with caution.
IGNORE_WHEN_COPYING_END