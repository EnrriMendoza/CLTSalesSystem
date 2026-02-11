# Sistema de Ventas CLT SA (CLTSalesSystem)

Este proyecto implementa un Sistema de Ventas para CLT SA utilizando los principios de **Clean Architecture** (Arquitectura Limpia) y tecnologías modernas de .NET.

## Tecnologías Utilizadas

- **.NET 8 API**: Framework principal para el backend.
- **Oracle Database 21c Express Edition**: Base de datos relacional.
- **Entity Framework Core 8**: ORM para el acceso a datos.
- **Fluent Validation**: Biblioteca para validación de modelos.
- **Serilog**: Biblioteca de logging estructurado.
- **JWT (JSON Web Tokens)**: Mecanismo de autenticación y autorización.
- **Swagger/OpenAPI**: Documentación de la API.

## Estructura del Proyecto

El proyecto sigue una arquitectura en capas claramente definidas:

1.  **CLTSalesSystem.Domain**: El núcleo del sistema. Contiene las entidades de negocio (`Producto`, `Cliente`, `Venta`, `DetalleVenta`), interfaces de repositorio y excepciones de dominio. No tiene dependencias externas.
2.  **CLTSalesSystem.Application**: Contiene la lógica de aplicación, casos de uso, DTOs, validaciones y servicios de aplicación. Orquesta las operaciones del dominio.
3.  **CLTSalesSystem.Infrastructure**: Implementa las interfaces definidas en el Dominio y Aplicación. Aquí reside la configuración de Entity Framework Core (`ApplicationDbContext`), migraciones, repositorios concretos y servicios de infraestructura (como logging).
4.  **CLTSalesSystem.API**: La capa de presentación y punto de entrada. Contiene los Controladores API, configuración de inyección de dependencias, middleware, autenticación y configuración de Swagger.

## Configuración

Para ejecutar el proyecto, es necesario configurar la cadena de conexión a la base de datos Oracle en el archivo `appsettings.json` o `appsettings.Development.json` ubicado en el proyecto `CLTSalesSystem.API`.

Ejemplo de configuración en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=mi_usuario;Password=mi_password;Data Source=localhost:1521/XEPDB1;"
  },
  "Jwt": {
    "Key": "tu_clave_secreta_super_segura_de_al_menos_32_caracteres",
    "Issuer": "CLTSalesSystem",
    "Audience": "CLTSalesSystemUsers"
  }
}
```

Asegúrate de que tu instancia de Oracle Express 21C esté corriendo y accesible.

## Ejecución

### Aplicar Migraciones

Antes de correr la aplicación por primera vez, debes aplicar las migraciones para crear la estructura de la base de datos.
Desde la raíz de la solución, ejecuta:

```bash
dotnet ef database update --project CLTSalesSystem.Infrastructure --startup-project CLTSalesSystem.API
```

O si prefieres ejecutar el script SQL manualmente, puedes encontrar el archivo `script.sql` generado en la raíz del proyecto.

### Correr el Proyecto

Para iniciar la API, ejecuta el siguiente comando desde la carpeta raíz o desde `CLTSalesSystem.API`:

```bash
dotnet run --project CLTSalesSystem.API
```

La aplicación se iniciará y escuchará en los puertos configurados (por defecto `http://localhost:5000` o `https://localhost:5001`).

## Endpoints Principales

Una vez que la aplicación esté corriendo, puedes acceder a la documentación interactiva de Swagger en:

- **Swagger UI**: `http://localhost:<puerto>/swagger/index.html`

Los controladores principales incluyen:

- **AuthController**: Registro y Login de usuarios (genera JWT).
- **ProductosController**: CRUD de productos. Requiere autenticación.
- **VentasController**: Registro y consulta de ventas. Requiere autenticación.
- **ClientesController**: Gestión de clientes.
