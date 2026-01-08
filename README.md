Descripción general

Este proyecto corresponde al módulo de gestión de productos desarrollado en C# con Windows Forms (.NET Framework) como parte de una práctica de pasantía, 
El sistema permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) sobre una base de datos SQL Server, mostrando la información en un DataGridView de forma dinámica.

El objetivo principal es demostrar el manejo de conexión a base de datos, eventos de interfaz gráfica y buenas prácticas básicas de desarrollo de aplicaciones de escritorio.

Tecnologías utilizadas
Lenguaje: C#
Plataforma: Windows Forms (.NET Framework)
Base de datos: SQL Server
Acceso a datos: System.Data.SqlClient

Estructura de la base de datos

Tabla principal utilizada:

Tabla: Producto
	•	ID_Producto (INT, IDENTITY, PRIMARY KEY)
	•	Codigo (NVARCHAR, UNIQUE, NOT NULL)
	•	Nombre (NVARCHAR)
	•	Existencia (INT)
	•	Estado (NVARCHAR)
	•	Proveedor (NVARCHAR)

El campo ID_Producto es autoincremental y es gestionado únicamente por la base de datos.

Funcionalidades del sistema

Insertar producto
	•	Permite registrar un nuevo producto ingresando los datos en los campos del formulario.
	•	El producto se almacena en la base de datos y se muestra automáticamente en el DataGridView.

Listar productos
	•	Muestra todos los productos registrados en la base de datos.
	•	Se ejecuta al cargar el formulario y mediante el botón Opciones.

Actualizar producto
	•	Permite modificar la información de un producto existente.
	•	La actualización se realiza utilizando el Código del producto como identificador.
	•	Los cambios se reflejan inmediatamente en el DataGridView.

Eliminar producto
	•	Permite eliminar un producto existente usando su Código.
	•	Solicita confirmación antes de realizar la eliminación.
	•	Actualiza automáticamente la vista del DataGridView.


Interfaz gráfica

La interfaz está compuesta por:
	•	Campos de texto para el ingreso de datos del producto.
	•	Botones para Insertar, Listar, Actualizar y Eliminar.
	•	Un DataGridView que muestra los productos almacenados.

El sistema garantiza que los cambios realizados se reflejen en tiempo real.


Ejecución del proyecto
	1.	Abrir el proyecto en Visual Studio.
	2.	Verificar la cadena de conexión a SQL Server.
	3.	Ejecutar el script de creación de la tabla Producto.
	4.	Ejecutar la aplicación.
	5.	Utilizar la interfaz para gestionar productos.

Conclusión

Este módulo cumple con los requerimientos básicos de una práctica de pasantía, demostrando el uso correcto de:
	•	Conexión a bases de datos
	•	Programación orientada a eventos
	•	Operaciones CRUD
	•	Interfaz gráfica en Windows Forms
El sistema es funcional, escalable y puede servir como base para módulos adicionales.


Autor: Guillermo
Práctica de Pasantía
