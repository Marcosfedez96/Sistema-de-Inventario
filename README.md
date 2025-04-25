 Sistema de Inventario  
Este proyecto permite la gestión de productos y stock en un sistema de inventario, facilitando el seguimiento de existencias y movimientos.  

## Tecnologías utilizadas  
- **C# y .NET** para la lógica backend  
- **SQL Server** para almacenamiento de productos y stock  
- **WPF** para la interfaz de usuario  

## Características  
 Registro y actualización de productos  
 Gestión de stock y alertas de baja existencia  
 Reportes de inventario y estadísticas  

## Cómo ejecutar el proyecto:    
1️⃣ Clonar el repositorio    
2️⃣ Configurar la base de datos en SQL Server    
  crear una nueva base de datos llamada SistemaDeInventario.  
  Con 2 tablas con este codigo:  
  
 CREATE TABLE Proveedor (  
    IdProveedor INT PRIMARY KEY,  
    NombreProveedor VARCHAR(255),  
    Contacto VARCHAR(50),  
    Email VARCHAR(255)  
);

CREATE TABLE Producto (  
    Idproducto INT PRIMARY KEY,  
    Nombre VARCHAR(255),      
    Categoria VARCHAR(100),  
    Precio DECIMAL(10,2),  
    Cantidad INT,  
    IdProveedor INT,  
    FOREIGN KEY (IdProveedor) REFERENCES Proveedor(IdProveedor)  
);
3️⃣ Ejecutar la aplicación en Visual Studio  

**Este sistema es ideal para pequeñas empresas o almacenes que necesitan un control preciso del inventario.** 
