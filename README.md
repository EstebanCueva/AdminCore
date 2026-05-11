<h1 align="center">AdminCore</h1>

<p align="center">
  Sistema administrativo MVC para la gestión de empresas, áreas, proveedores y presupuestos por área.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/STATUS-EN%20DESARROLLO-green">
  <img src="https://img.shields.io/badge/.NET-8.0-blue">
  <img src="https://img.shields.io/badge/ASP.NET%20Core-MVC-purple">
  <img src="https://img.shields.io/badge/Database-SQL%20Server-red">
</p>

---

## Índice

- [Descripción del proyecto](#descripción-del-proyecto)
- [Estado del proyecto](#estado-del-proyecto)
- [Funcionalidades](#funcionalidades)
- [Core del sistema](#core-del-sistema)
- [Validación Back-End](#validación-back-end)
- [Relaciones entre tablas](#relaciones-entre-tablas)
- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Acceso al proyecto](#acceso-al-proyecto)
- [Cómo ejecutar el proyecto](#cómo-ejecutar-el-proyecto)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Base de datos](#base-de-datos)
- [Deploy](#deploy)
- [Autor](#autor)
- [Licencia](#licencia)

---

## Descripción del proyecto

**AdminCore** es un sistema administrativo desarrollado con **ASP.NET Core MVC** para gestionar la configuración base de una plataforma de control de gastos empresariales.

El sistema está orientado a empresas, emprendimientos o pequeños negocios que necesitan organizar su estructura interna mediante empresas, áreas o departamentos, proveedores y presupuestos mensuales por área.

En esta primera versión, el sistema se centra únicamente en el módulo administrativo. Los gastos no son registrados por el administrador, sino que serán ingresados posteriormente por usuarios operativos en otro módulo del sistema.

---

## Estado del proyecto

🚧 Proyecto en desarrollo 🚧

Actualmente el sistema cuenta con el módulo administrativo funcional en entorno local.

---

## Funcionalidades

### Funcionalidades implementadas

- Dashboard administrativo con resumen general.
- Gestión de empresas.
- Gestión de áreas asociadas a empresas.
- Gestión de proveedores asociados a empresas.
- Gestión de presupuestos mensuales por área.
- Validación Back-End del RUC.
- Uso de dropdowns para relaciones entre tablas.
- Dropdown dependiente entre empresa y área.
- Validación en Back-End para verificar que el área pertenezca a la empresa seleccionada.

### Funcionalidades futuras

- Registro de gastos por parte de usuarios.
- Reportes de gastos por área.
- Comparación entre presupuesto asignado y gasto real.
- Alertas cuando un área supere su presupuesto.
- Login y roles de usuario.
- Dashboard financiero con gráficos.

---

## Core del sistema

El core del sistema está enfocado en la **gestión administrativa de gastos empresariales por áreas**.

El administrador configura la estructura base del sistema:

- Empresas.
- Áreas o departamentos.
- Proveedores.
- Presupuestos por área.

Posteriormente, los usuarios del sistema podrán registrar gastos reales, permitiendo comparar el gasto ejecutado contra el presupuesto asignado por el administrador.

El objetivo principal es ayudar a las empresas o emprendimientos a controlar mejor el dinero asignado a cada área.

---

## Validación Back-End

El sistema cumple con el requisito de validar un dato sensible desde el Back-End.

### Dato sensible validado

El dato sensible elegido es el **RUC**, ya que identifica legalmente a una empresa o proveedor.

### Reglas de validación

Antes de guardar una empresa o proveedor, el Back-End valida que:

- El RUC no esté vacío.
- El RUC tenga 13 dígitos.
- El RUC contenga solo números.
- Los últimos 3 dígitos no sean `000`.
- El RUC no esté duplicado en la base de datos.

Esta validación no depende únicamente de JavaScript, sino que se realiza en el servidor antes de guardar la información.

---

## Relaciones entre tablas

El sistema evita ingresar claves foráneas manualmente.

### Ejemplos implementados

Al crear un área:

```txt
Empresa: [Dropdown]
Nombre del área: [Input]
```

Al crear un proveedor:

```txt
Empresa: [Dropdown]
Nombre proveedor: [Input]
RUC: [Input]
```

Al crear un presupuesto por área:

```txt
Empresa: [Dropdown]
Área: [Dropdown dependiente de la empresa]
Mes: [Dropdown]
Año: [Input]
Monto asignado: [Input]
```

Además, el Back-End valida que el área seleccionada pertenezca realmente a la empresa seleccionada.

---

## Tecnologías utilizadas

- ASP.NET Core MVC
- .NET 8
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap
- JavaScript
- Git
- GitHub
- Azure App Service
- Azure SQL Database

---

## Acceso al proyecto

Repositorio en GitHub:

```txt
https://github.com/EstebanCueva/AdminCore
```

URL del sistema desplegado:

```txt
PENDIENTE_AGREGAR_URL_DE_AZURE
```

---

## Cómo ejecutar el proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/EstebanCueva/AdminCore.git
```

### 2. Entrar a la carpeta del proyecto

```bash
cd AdminCore
```

### 3. Configurar la cadena de conexión

En el archivo `appsettings.json`, configurar la conexión a SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=AdminCoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

También puede usarse LocalDB:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AdminCoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 4. Restaurar paquetes

```bash
dotnet restore
```

### 5. Aplicar migraciones

```bash
dotnet ef database update
```

También se puede ejecutar desde Package Manager Console en Visual Studio:

```powershell
Update-Database
```

### 6. Ejecutar el proyecto

```bash
dotnet run
```

O desde Visual Studio:

```txt
Ctrl + F5
```

### 7. Acceder al sistema

```txt
https://localhost:PUERTO/Dashboard
```

---

## Estructura del proyecto

```txt
AdminCore
│
├── Controllers
│   ├── DashboardController.cs
│   ├── EmpresasController.cs
│   ├── AreasEmpresaController.cs
│   ├── ProveedoresController.cs
│   └── PresupuestosAreaController.cs
│
├── Data
│   └── AppDbContext.cs
│
├── Models
│   └── Admin
│       ├── Empresa.cs
│       ├── AreaEmpresa.cs
│       ├── Proveedor.cs
│       └── PresupuestoArea.cs
│
├── Services
│   ├── IRucValidator.cs
│   └── RucValidator.cs
│
├── Views
│   ├── Dashboard
│   ├── Empresas
│   ├── AreasEmpresa
│   ├── Proveedores
│   ├── PresupuestosArea
│   └── Shared
│
├── wwwroot
│
├── appsettings.json
├── Program.cs
└── README.md
```

---

## Base de datos

El sistema utiliza SQL Server y Entity Framework Core.

### Tablas principales

- Empresas
- AreasEmpresa
- Proveedores
- PresupuestosArea
- __EFMigrationsHistory

### Relaciones principales

```txt
Empresa 1 --- * AreasEmpresa
Empresa 1 --- * Proveedores
Empresa 1 --- * PresupuestosArea
AreaEmpresa 1 --- * PresupuestosArea
```

---

## Deploy

El proyecto será desplegado en Azure utilizando:

- Azure App Service para hospedar la aplicación.
- Azure SQL Database para hospedar la base de datos.
- GitHub para versionamiento del código.

La cadena de conexión de producción debe configurarse en Azure App Service como variable de entorno, evitando dejar credenciales sensibles directamente en el repositorio.

---

## Autor

| Nombre | GitHub |
|---|---|
| Esteban Cueva | [@EstebanCueva](https://github.com/EstebanCueva) |

---

## Licencia

Este proyecto fue desarrollado con fines académicos.

Actualmente no cuenta con una licencia de código abierto definida.
