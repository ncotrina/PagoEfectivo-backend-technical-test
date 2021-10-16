# PagoEfectivo-backend-technical
Este proyecto web se ejecuta sobre ASP NET Core con EntityFrameworkCore.

### Requerimentos ###
- Visual Studio 2019
- SQL Server
- IIS Express

### Cadena Conexion SQL Server ###

```
Data Source=(localdb)\\.;Initial Catalog=PagoEfectivo;Integrated Security=true
```

### Instrucciones ###

1. Ejecutar los siguientes comandos, para crear la Bd y generar migraciones nuevamente:
	update-database
2. Verificar el nombre de la cadena de conexion (puede cambiarla)	
3. Abrir solución (.sln) con Visual Studio tanto para Backend y Frontend
4. Si hubiera algun problema con los nugets, restaurarlos desde la solucion de los proyectos
5. Verificar la url de los endpoints en el proyecto de Frontend, en el archivo site.js
