using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sistema_de_Inventario;

public class Proveedor
{
    private int _idProveedor;
    private string _nombreProveedor;
    private string _contacto;
    private string _telefono;

    public int IDProveedor { get { return _idProveedor; } set { _idProveedor = value; } }
    public string NombreProveedor { get { return _nombreProveedor; } set { _nombreProveedor = value; } }
    public string Contacto { get { return _contacto; } set { _contacto = value; } }
    public string Telefono { get { return _telefono; } set { _telefono = value; } }

    public void agregarProveedor(Proveedor proveedor)
    {
        DataBaseConnection dataBaseConnection = new DataBaseConnection();
        dataBaseConnection.AbrirConeccionSql();
        string query = "insert into Proveedor(IdProveedor, NombreProveedor,Contacto,Telefono) " +
            "values (@idProveedor,@nombreProveedor,@contacto,@telefono);";
        SqlCommand cmd = new SqlCommand(query, dataBaseConnection.GetCliente());
        cmd.Parameters.AddWithValue("@idProveedor",proveedor._idProveedor);
        cmd.Parameters.AddWithValue("@nombreProveedor",proveedor._nombreProveedor);
        cmd.Parameters.AddWithValue("@contacto",proveedor._contacto);
        cmd.Parameters.AddWithValue("@telefono",proveedor._telefono);
        cmd.ExecuteNonQuery();
        dataBaseConnection.CerrarConeccionSql();
    }

    public Proveedor BuscarProveedor(int idProveedor)
    {

        DataBaseConnection dataBaseConnection = new DataBaseConnection();
        dataBaseConnection.AbrirConeccionSql();
        string query = "SELECT NombreProveedor,Contacto,Telefono FROM proveedor WHERE IdProveedor = @IdProveedor;";
        SqlCommand cmd = new SqlCommand(query, dataBaseConnection.GetCliente());
        try
        {
            cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                this._nombreProveedor = reader.GetString(0);
                this._contacto = reader.GetString(1);
                this._telefono = reader.GetString(2);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return this;
    }

}
