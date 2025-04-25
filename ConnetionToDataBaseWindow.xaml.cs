using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sistema_de_Inventario
{
    /// <summary>
    /// Lógica de interacción para ConnetionToDataBaseWindow.xaml
    /// </summary>
    public partial class ConnetionToDataBaseWindow : Window
    {
        public ConnetionToDataBaseWindow()
        {
            InitializeComponent();
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.CargarStringConnection();
            text_data_sourse.Text = dataBaseConnection.dataSourse;
            text_Initial_Catalog.Text = dataBaseConnection.initialCatalog;

            this.Closed += WinClose;

        }

        private void WinClose(object? sender , EventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void button_Connection_database_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConnection dataBaseConnection = new DataBaseConnection();
            dataBaseConnection.dataSourse = text_data_sourse.Text;
            dataBaseConnection.initialCatalog = text_Initial_Catalog.Text;
            dataBaseConnection.AbrirConeccionSql();
            dataBaseConnection.GuardarStringConnection();
            GestorDeInventario gestorDeInventario = new GestorDeInventario();
            gestorDeInventario.Show();
            dataBaseConnection.CerrarConeccionSql();
            this.Hide();
        }

       
    }  
}
