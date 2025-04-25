using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Inventario;

public class Producto
{
    private int _idProducto;
    private string _nombreProducto;
    private string _categoria;
    private decimal _precio;
    private int _cantidad;
    private Proveedor _proveedor = new Proveedor();

    public int IdProveedor { get { return _proveedor.IDProveedor; } set { _proveedor.IDProveedor = value; } }

    public int IdProducto {  get { return _idProducto; } set { _idProducto = value; } }
    public string NombreProducto { get { return _nombreProducto; } set { _nombreProducto = value; } }
    public string Categoria { get { return _categoria; } set { _categoria = value; } }
    public decimal Precio { get { return _precio; } set { _precio = value; } }
    public int Cantidad { get { return _cantidad; }set{ _cantidad = value; } }
    
    public void AgregarProducto()
    {
        DataBaseConnection dataBaseConnection = new DataBaseConnection();

        dataBaseConnection.AbrirConeccionSql();
        string query = "INSERT INTO Productos(Idproducto,Nombre,Categoria,Precio,Cantidad,IdProveedor) VALUES (@idProducto,@nombre,@categoria,@precio,@cantidad,@idProveedor);";
        SqlCommand cmd = new SqlCommand(query, dataBaseConnection.GetCliente());
        cmd.Parameters.AddWithValue("@idProducto", _idProducto);
        cmd.Parameters.AddWithValue("@nombre", _nombreProducto);
        cmd.Parameters.AddWithValue("@categoria", _categoria);
        cmd.Parameters.AddWithValue("@precio", _precio);
        cmd.Parameters.AddWithValue("@cantidad", _cantidad);
        cmd.Parameters.AddWithValue("@idProveedor", _proveedor.IDProveedor);
        cmd.ExecuteNonQuery();
        dataBaseConnection.CerrarConeccionSql();
    }

    public Producto buscarProductoID(int idProducto)
    {
        DataBaseConnection dataBaseConnection = new DataBaseConnection();
        dataBaseConnection.AbrirConeccionSql();
        string query = "SELECT Nombre, Categoria, Precio, Cantidad FROM Productos where idproducto = @idProducto;";
        SqlCommand cmd = new SqlCommand(query, dataBaseConnection.GetCliente());
        cmd.Parameters.AddWithValue("@idProducto", idProducto);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read()) {
            this._nombreProducto = reader.GetString(0);
            this._categoria = reader.GetString(1);
            this._precio = reader.GetDecimal(2);
            this._cantidad = reader.GetInt32(3);

        }
        reader.Close();

        return this;
    }

    public List<Producto> BuscarPorNombre(string nombre)
    {
        List<Producto> list = new List<Producto>();
        DataBaseConnection dataBaseConnection = new DataBaseConnection();
        dataBaseConnection.AbrirConeccionSql();
        string query = "SELECT * FROM Productos where Nombre = @nombre;";
        SqlCommand cmd = new SqlCommand(query, dataBaseConnection.GetCliente());
        cmd.Parameters.AddWithValue("@nombre",nombre);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Producto producto = new Producto();
            producto._idProducto = reader.GetInt32(0);
            producto._nombreProducto = reader.GetString(1);
            producto._categoria = reader.GetString(2);
            producto._precio = reader.GetDecimal(3);
            producto._cantidad = reader.GetInt32(4);
            producto.IdProveedor = reader.GetInt32(5);
            list.Add(producto);
       
        }
        reader.Close();
        dataBaseConnection.CerrarConeccionSql();
        return list;
    }

    public List<Producto> BuscarPorCategoria(string categoria)
    {
        List<Producto> list = new List<Producto>();
        DataBaseConnection dataBaseConnection =     new DataBaseConnection();
        dataBaseConnection.AbrirConeccionSql();
        string query = "SELECT * FROM Productos where Categoria = @categoria;";
        SqlCommand cmd = new SqlCommand(query,dataBaseConnection.GetCliente());
        cmd.Parameters.AddWithValue("@categoria", categoria);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read()) {
            Producto producto = new Producto()
            {
                IdProducto = reader.GetInt32(0),
                NombreProducto = reader.GetString(1),
                Categoria = reader.GetString(2),
                Precio = reader.GetDecimal(3),
                Cantidad = reader.GetInt32(4),
                IdProveedor = reader.GetInt32(5)
            };
            list.Add(producto);
        }
        reader.Close();
        return list;
    }

    public List<Producto> ListarInventario(string idProveedorlist)
    {
        List<Producto> list = new List<Producto>();
        DataBaseConnection databaseConnection = new DataBaseConnection();   
        databaseConnection.AbrirConeccionSql();
        string query = "SELECT * FROM Productos WHERE IdProveedor = @idproveedor; ";
        SqlCommand cmd = new SqlCommand(query,databaseConnection.GetCliente());
        cmd.Parameters.AddWithValue("@idproveedor", idProveedorlist);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read()) {
            Producto producto = new()
            {
                IdProducto = reader.GetInt32(0),
                NombreProducto = reader.GetString(1),
                Categoria = reader.GetString(2),
                Precio = reader.GetDecimal(3),
                Cantidad = reader.GetInt32(4),
                IdProveedor = reader.GetInt32(5),
                
            };
            list.Add(producto);
        }
        reader.Close();
        databaseConnection.CerrarConeccionSql();

        return list;
    }

    public void ActualizarCantidades(Producto producto)
    {

    }
}
