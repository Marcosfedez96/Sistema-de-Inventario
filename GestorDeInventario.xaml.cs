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
    /// Lógica de interacción para GestorDeInventario.xaml
    /// </summary>
    public partial class GestorDeInventario : Window
    {
        public GestorDeInventario()
        {
            InitializeComponent();
            HabilitarCamposProductos("Actualizado");

            this.Closed += GestorDeInventario_Closed;
        }

        private void GestorDeInventario_Closed(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        #region ZONA PRODUCTO


        private void button_buscar_producto_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = new Producto();
            text_nombre_producto.Foreground = new SolidColorBrush(Colors.Black);
            text_categoria_producto.Foreground = new SolidColorBrush(Colors.Black);
            text_precio_producto.Foreground = new SolidColorBrush(Colors.Black);
            text_cantidad_producto.Foreground = new SolidColorBrush(Colors.Black);

            VaciarProductos();

            producto.buscarProductoID(int.Parse(text_id_producto.Text));
            if (producto.NombreProducto != null)
            {
                HabilitarCamposProductos("BusquedaExitosa");
                text_nombre_producto.Text = producto.NombreProducto;
                text_categoria_producto.Text = producto.Categoria;
                text_precio_producto.Text = producto.Precio.ToString();
                text_cantidad_producto.Text = producto.Cantidad.ToString();
            }
            else
            {
                HabilitarCamposProductos("BusquedaFallida");
                
                FinProcesoProductos();
            }

        }

        private void button_agregar_datos_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {

                Producto producto = new Producto();
                //producto.Proveedor = new Proveedor();
                producto.IdProveedor = int.Parse(text_id_proveedor.Text);
                producto.IdProducto = int.Parse(text_id_producto.Text);
                producto.NombreProducto = text_nombre_producto.Text;
                producto.Categoria = text_categoria_producto.Text;
                producto.Precio = decimal.Parse(text_precio_producto.Text);
                producto.Cantidad = int.Parse(text_cantidad_producto.Text);
                producto.AgregarProducto();
                HabilitarCamposProductos("Registrado");
                FinProcesoProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button_actualizar_datos_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCamposProductos("Actualizado");
            FinProcesoProductos();
        }

        private void VaciarProductos()
        {
            text_categoria_producto.Text = "";
            text_nombre_producto.Text = "";
            text_precio_producto.Text = "";
            text_cantidad_producto.Text = "";
        }

        private void HabilitarCamposProductos(string tipoDeLlamada)
        {
            if (tipoDeLlamada == "BusquedaFallida")
            {
                text_precio_producto.IsEnabled = true;
                text_cantidad_producto.IsEnabled = true;
                text_nombre_producto.IsEnabled = true;
                text_categoria_producto.IsEnabled = true;
                button_agregar_datos.IsEnabled = true;
                button_actualizar_datos.IsEnabled = false;

            }
            else if (tipoDeLlamada == "BusquedaExitosa")
            {
                text_nombre_producto.IsEnabled = false;
                text_categoria_producto.IsEnabled = false ;
                text_precio_producto.IsEnabled = true;
                text_cantidad_producto.IsEnabled = true;
                button_agregar_datos.IsEnabled = false;
                button_actualizar_datos.IsEnabled = true;

            }
            else if (tipoDeLlamada == "Actualizado" || tipoDeLlamada == "Registrado"){
                text_precio_producto.IsEnabled = false;
                text_cantidad_producto.IsEnabled = false;
                text_nombre_producto.IsEnabled = false;
                text_categoria_producto.IsEnabled = false;
                button_agregar_datos.IsEnabled = false;
                button_actualizar_datos.IsEnabled = false;
            }



        }
        
        public void FinProcesoProductos()
        {
            VaciarProductos();
            LostFocus(text_nombre_producto, "Nombre Producto");
            LostFocus(text_categoria_producto, "Categoria Producto");
            LostFocus(text_precio_producto, "Precio Producto");
            LostFocus(text_cantidad_producto, "Cantidad Producto");
        }

        #endregion


        #region ZONA PROVEEDOR

        private void button_buscar_proveedor_Click(object sender, RoutedEventArgs e)
        {
            CargarProveedor();

        }

        private void text_id_proveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CargarProveedor();
            }

        }
        private void CargarProveedor()
        {
            text_nombre.Text = "";
            text_contacto.Text = "";
            text_telefono.Text = "";
            Proveedor proveedor = new Proveedor();
            proveedor.BuscarProveedor(int.Parse(text_id_proveedor.Text));
            if (proveedor.NombreProveedor == null)
            {
                text_nombre.IsEnabled = true;
                text_contacto.IsEnabled = true;
                text_telefono.IsEnabled = true;
                button_rgistrar_proveedor.IsEnabled = true;

                LostFocus(text_nombre, "Nombre");
                LostFocus(text_contacto, "Contacto");
                LostFocus(text_telefono, "Telefono");
            }
            else
            {
                text_nombre.IsEnabled = false;
                text_contacto.IsEnabled = false;
                text_telefono.IsEnabled = false;
                button_rgistrar_proveedor.IsEnabled = false;
                text_nombre.Text = proveedor.NombreProveedor;
                text_contacto.Text = proveedor.Contacto;
                text_telefono.Text = proveedor.Telefono;
            }

        }
        private void button_rgistrar_proveedor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Proveedor proveedor = new Proveedor();
                proveedor.IDProveedor = int.Parse(text_id_proveedor.Text);
                proveedor.NombreProveedor = text_nombre.Text;
                proveedor.Contacto = text_contacto.Text;
                proveedor.Telefono = text_telefono.Text;
                proveedor.agregarProveedor(proveedor);
                text_nombre.Text = "";
                text_contacto.Text = "";
                text_telefono.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion


        #region EVENTOS DE GOT/LEFTFOCUS 

        public void GotFocus(TextBox textBox, string texto)
        {
            if (textBox.Text == texto)
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        public void LostFocus(TextBox textBox, string texto)
        {
            if (textBox.Text == "" || textBox.Text == texto)
            {
                textBox.Text = texto;
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void text_id_proveedor_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_id_proveedor, "Id Proveedor");

        }

        private void text_id_proveedor_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_id_proveedor, "Id Proveedor");

        }

        private void text_nombre_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_nombre, "Nombre");
        }

        private void text_nombre_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_nombre, "Nombre");
        }

        private void text_telefono_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_telefono, "Telefono");
        }

        private void text_telefono_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_telefono, "Telefono");
        }

        private void text_contacto_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_contacto, "Contacto");
        }

        private void text_contacto_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_contacto, "Contacto");

        }
        private void text_id_producto_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_id_producto, "Id Producto");
        }

        private void text_id_producto_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_id_producto, "Id Producto");
        }

        private void text_nombre_producto_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_nombre_producto, "Nombre Producto");
        }

        private void text_nombre_producto_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_nombre_producto, "Nombre Producto");
        }

        private void text_categoria_producto_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_categoria_producto, "Categoria Producto");
        }

        private void text_categoria_producto_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_categoria_producto, "Categoria Producto");

        }

        private void text_precio_producto_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_precio_producto, "Precio Producto");
        }

        private void text_precio_producto_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_precio_producto, "Precio Producto");
        }

        private void text_cantidad_producto_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus(text_cantidad_producto, "Cantidad Producto");
        }

        private void text_cantidad_producto_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocus(text_cantidad_producto, "Cantidad Producto");
        }




        #endregion

        #region TAB BUSCAR

        private void button_caja_busqueda_Click(object sender, RoutedEventArgs e)
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = new();
            
            switch (combobox_busqueda_productos.Text)
            {
                case "Nombre":
                    productos = producto.BuscarPorNombre(text_caja_busqueda.Text);
                    datagrid_busqueda_productos.ItemsSource = productos;
                    break;
                case "Categoria":
                    productos = producto.BuscarPorCategoria(text_caja_busqueda.Text);
                    datagrid_busqueda_productos.ItemsSource = productos;
                    break;
            }
            

        }

        #endregion

        private void button_listar_Click(object sender, RoutedEventArgs e)
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = new();
            productos = producto.ListarInventario(textbox_listar.Text);
           datagrid_listar.ItemsSource= productos;
        }
    }
}
