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
Se requiere:
Visual Studio Version 17  
VisualStudioVersion = 17.13.35919.96 d17.13   
MinimumVisualStudioVersion = 10.0.40219.1   
Framework .NET 8.0   
WPF   
Dependencias:     
Microsoft.Data.SqlClient  

2️⃣ Configurar la base de datos en SQL Server management studio     
  crear una nueva base de datos llamada SistemaDeInventario.     
     
  create database SistemaDeInventario;  
  
  Con 2 tablas con este codigo:    
     
 CREATE TABLE proveedores (  
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
mas adelante are la parte de ventas de productos que conectara con la misma base de datos. 


![Muestra2](https://github.com/user-attachments/assets/722d5a3d-110b-44f8-820d-bec9f0d79720)
## Se puede editar las cantidades y precios de productos registrados:
![image](https://github.com/user-attachments/assets/a010917f-7241-4643-bb57-8a9e1b2708a0)
## Si el proveedor no existe, se habilitan las casillas para que se puedan registrar:
![image](https://github.com/user-attachments/assets/dd19a9a1-998a-4580-8836-a5a62ca7162d)
## Si el producto no existe, se habilitan las casillas para que se puedan registrar:
![image](https://github.com/user-attachments/assets/418cb5bd-b559-40db-877c-808041f64a9c)
## Se Puede buscar por el nombre del producto:
![image](https://github.com/user-attachments/assets/5720aa67-720d-43ef-8ac3-ff93915c6fab)
## Tambien por la categoria del producto:
![image](https://github.com/user-attachments/assets/38ea39d7-aed7-462c-844a-daf9f2f1f043)
## Se puede listar todos los productos del proveedor que busques:
![image](https://github.com/user-attachments/assets/3a0ad062-11d1-4a24-95e0-869e98f89cfd)
