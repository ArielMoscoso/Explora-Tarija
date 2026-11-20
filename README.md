# 🌄 Explora Tarija

**API REST para la gestión turística de Tarija**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-8.0-512BD4)](https://learn.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-Compatible-CC2927?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![Swagger](https://img.shields.io/badge/Swagger-Documentado-85EA2D?logo=swagger&logoColor=black)](https://swagger.io/)

Explora Tarija es una API desarrollada con ASP.NET Core para administrar usuarios, empresas, categorías de servicios, reservas y pagos relacionados con experiencias turísticas.

---

## ✨ Características

- API REST organizada con controladores y DTOs.
- Persistencia con Entity Framework Core y SQL Server.
- Migraciones listas para crear y actualizar la base de datos.
- Validación de datos mediante Data Annotations.
- Documentación interactiva con Swagger/OpenAPI.
- Operaciones para listar y filtrar registros.
- Creación y actualización de usuarios, empresas y categorías.
- Registro de pagos asociados a reservas existentes.

---

## 🧱 Tecnologías

| Tecnología | Uso |
|---|---|
| ASP.NET Core 8 | Framework principal de la API |
| C# | Lenguaje de programación |
| Entity Framework Core 8 | ORM y acceso a datos |
| SQL Server / LocalDB | Motor de base de datos |
| Swagger / Swashbuckle | Documentación y prueba de endpoints |

---

## 📂 Estructura del proyecto

```text
Explora-Tarija/
├── Controllers/       # Endpoints HTTP
├── DTO/               # Objetos de entrada y salida
├── Data/              # DbContext de Entity Framework
├── Entidades/         # Modelos de la base de datos
├── Enumeraciones/     # Estados de pago y reserva
├── Migrations/        # Historial de migraciones
├── Properties/        # Configuración de ejecución
├── Program.cs         # Configuración de servicios y pipeline
└── appsettings.json   # Cadena de conexión y configuración
```

---

## 🗃️ Modelo de datos

La base de datos contiene las siguientes entidades principales:

- **Usuarios:** nombre, apellido, CI y celular.
- **Empresas:** nombre, teléfono, correo y dirección.
- **Categorías:** clasificación de productos o servicios.
- **Catálogo:** productos publicados por las empresas.
- **Reservas:** reservas realizadas por usuarios.
- **Pagos:** pagos vinculados a una reserva.

Las tablas y relaciones se configuran en `Data/AppDbContext.cs` y se crean mediante las migraciones de Entity Framework.

---

## 🚀 Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server o SQL Server LocalDB
- Visual Studio 2022 o Visual Studio Code

> En Windows, la configuración predeterminada utiliza LocalDB. En otros entornos, reemplaza la cadena de conexión por una instancia SQL Server accesible.

---

## ⚙️ Instalación y ejecución

### 1. Clona el repositorio

```bash
git clone https://github.com/ArielMoscoso/Explora-Tarija.git
cd Explora-Tarija
```

### 2. Configura la base de datos

Verifica la cadena `DefaultConnection` en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ExploraTarija_DB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Aplica las migraciones

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

También puedes configurar una cadena de conexión mediante variable de entorno:

```bash
export ConnectionStrings__DefaultConnection="Server=localhost;Database=ExploraTarija_DB;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=True"
```

### 4. Ejecuta el proyecto

```bash
dotnet run
```

Al iniciar en modo Development, abre Swagger en:

- HTTP: [http://localhost:5294/swagger](http://localhost:5294/swagger)
- HTTPS: [https://localhost:7025/swagger](https://localhost:7025/swagger)

---

## 🔌 Endpoints principales

| Recurso | Método | Ruta | Descripción |
|---|---|---|---|
| Usuarios | `GET` | `/api/Usuarios` | Lista todos los usuarios |
| Usuarios | `GET` | `/api/Usuarios/FiltrarUsuario?nombreUsuario=Ana` | Filtra usuarios por nombre |
| Usuarios | `POST` | `/api/Usuarios` | Registra un usuario |
| Usuarios | `PUT` | `/api/Usuarios/{id}` | Actualiza un usuario |
| Categorías | `GET` | `/api/Categorias` | Lista todas las categorías |
| Categorías | `GET` | `/api/Categorias/FiltrarCategoria?nombreCategoria=Hotel` | Filtra categorías por nombre |
| Categorías | `POST` | `/api/Categorias` | Crea una categoría |
| Categorías | `PUT` | `/api/Categorias/{id}` | Actualiza una categoría |
| Empresas | `GET` | `/api/Empresas` | Lista todas las empresas |
| Empresas | `GET` | `/api/Empresas/FiltrarEmpresa?nombreEmpresa=Tarija` | Filtra empresas por nombre |
| Empresas | `POST` | `/api/Empresas` | Registra una empresa |
| Empresas | `PUT` | `/api/Empresas/{id}` | Actualiza una empresa |
| Pagos | `GET` | `/api/Pagos` | Lista todos los pagos |
| Pagos | `GET` | `/api/Pagos/FiltrarMetodoPago?metodoPago=QR` | Filtra pagos por método |
| Pagos | `POST` | `/api/Pagos` | Registra un pago |

---

## 🧪 Ejemplos de uso

### Crear usuario

```http
POST /api/Usuarios
Content-Type: application/json

{
  "nombre": "Ana",
  "apellido": "Mendoza",
  "ci": 12345678,
  "celular": 70000000
}
```

### Crear categoría

```http
POST /api/Categorias
Content-Type: application/json

{
  "nombreCategoria": "Hospedaje"
}
```

### Crear empresa

```http
POST /api/Empresas
Content-Type: application/json

{
  "nombreEmpresa": "Aventuras Tarija",
  "telefonoEmpresa": 46600000,
  "correoEmpresa": "contacto@aventuras.com",
  "direccionEmpresa": "Centro, Tarija"
}
```

### Registrar pago

```http
POST /api/Pagos
Content-Type: application/json

{
  "metodoPago": "QR",
  "monto": 150.50,
  "idReserva": 1
}
```

---

## 🛠️ Comandos útiles

```bash
# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run

# Crear una nueva migración
dotnet ef migrations add NombreDeLaMigracion

# Aplicar migraciones
dotnet ef database update

# Eliminar la última migración pendiente
dotnet ef migrations remove
```

---

## 🤝 Contribución

1. Crea una rama desde `main`.
2. Realiza cambios pequeños y claros.
3. Compila y prueba la aplicación.
4. Envía un pull request describiendo tu aporte.

---

<div align="center">

**Hecho con ❤️ para promover y organizar el turismo en Tarija**

</div>
