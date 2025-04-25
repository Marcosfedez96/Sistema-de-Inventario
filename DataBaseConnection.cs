using Microsoft.Data.SqlClient.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.IO;
using System.DirectoryServices;

namespace Sistema_de_Inventario;

public class DataBaseConnection
{
    private string _dataSourse;
    private string _initialCatalog;
    private string ruta = "Connection Data/StringConnectionData.txt";
    SqlConnection client;

    public string dataSourse { get { return _dataSourse; } set { _dataSourse = value; } }
    public string initialCatalog { get { return _initialCatalog; } set { _initialCatalog = value; } }

    public SqlConnection GetCliente()
    {
              
        return client;

    }

    public void AbrirConeccionSql()
    {
        try
        {
            
            if(client == null)
            {
                CargarStringConnection();
            }
            if (client.State == System.Data.ConnectionState.Closed)
            {
                client.Open();
                MessageBox.Show("Coneccion Exitosa!");
            }

        }
        catch (Exception ex) {
        MessageBox.Show(ex.Message);
        }
        

    }
    public void CerrarConeccionSql()
    {
        try
        {
            SqlConnection cliente = GetCliente();
            if (cliente.State == System.Data.ConnectionState.Open)
            {
                cliente.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
       
    }
  
    public void GuardarStringConnection()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine(_dataSourse);
                sw.WriteLine(_initialCatalog);
            }
        }
        catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }


    }

    public void CargarStringConnection()
    {
           
        using(StreamReader sr = new StreamReader(ruta))
        { 
            _dataSourse = sr.ReadLine();
            _initialCatalog = sr.ReadLine();

            string stringConnection = $"Data Source={_dataSourse};Initial Catalog={_initialCatalog};Integrated Security=True;Trust Server Certificate=True";
            client = new SqlConnection(stringConnection);
        }

    }
}
