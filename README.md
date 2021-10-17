# PagoEfectivo-backend-technical
Este proyecto web se ejecuta sobre ASP NET Core con EntityFrameworkCore.

### Requerimentos ###
- Visual Studio 2019
- SQL Server
- IIS Express

### Cadena Conexion SQL Server ###

```
Data Source=.;Initial Catalog=PagoEfectivo;Integrated Security=true
```

### Instrucciones ###
1. Ubicarse en la rama master
2. Ejecutar los siguientes comandos, para crear la Bd y generar migraciones nuevamente:
	update-database
3. Verificar el nombre de la cadena de conexion rn el proyecto de backend (puede cambiarla)	
4. Abrir solución (.sln) con Visual Studio tanto para Backend y Frontend
5. Si hubiera algún problema con los nugets, restaurarlos desde la solucion de los proyectos
6. Verificar la url de los endpoints en el proyecto de Frontend, en el archivo site.js
