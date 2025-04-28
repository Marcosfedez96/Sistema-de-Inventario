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
       

        private void ChangeColor()
        {
            text_nombre_producto.Foreground = new SolidColorBrush(Colors.Black);
            text_categoria_producto.Foreground = new SolidColorBrush(Colors.Black);
            text_precio_producto.Foreground = new SolidColorBrush(Colors.Black);
            text_cantidad_producto.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void VaciarProductos()
        {
            text_categoria_producto.Text = "";
            text_nombre_producto.Text = "";
            text_precio_producto.Text = "";
            text_cantidad_producto.Text = "";
        }
        private Producto CrearProducto(Producto producto)
        {
            try
            {
                producto.IdProveedor = int.Parse(text_id_proveedor.Text);
                producto.IdProducto = int.Parse(text_id_producto.Text);
                producto.NombreProducto = text_nombre_producto.Text;
                producto.Categoria = text_categoria_producto.Text;
                producto.Precio = decimal.Parse(text_precio_producto.Text);
                producto.Cantidad = int.Parse(text_cantidad_producto.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return producto;
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
                text_categoria_producto.IsEnabled = false;
                text_precio_producto.IsEnabled = true;
                text_cantidad_producto.IsEnabled = true;
                button_agregar_datos.IsEnabled = false;
                button_actualizar_datos.IsEnabled = true;

            }
            else if (tipoDeLlamada == "Actualizado" || tipoDeLlamada == "Registrado")
            {
                text_precio_producto.IsEnabled = false;
                text_cantidad_producto.IsEnabled = false;
                text_nombre_producto.IsEnabled = false;
                text_categoria_producto.IsEnabled = false;
                button_agregar_datos.IsEnabled = false;
                button_actualizar_datos.IsEnabled = false;
            }



        }
        private void VaciarProveedor()
        {
            text_nombre.Text = "";
            text_contacto.Text = "";
            text_telefono.Text = "";
        }
        private void ProveedorExiste( Proveedor proveedor)
        {
            
            text_nombre.IsEnabled = false;
            text_contacto.IsEnabled = false;
            text_telefono.IsEnabled = false;
            button_rgistrar_proveedor.IsEnabled = false;
            text_nombre.Text = proveedor.NombreProveedor;
            text_contacto.Text = proveedor.Contacto;
            text_telefono.Text = proveedor.Telefono;
        }
        private void ProveedorNoExiste()
        {
            text_nombre.IsEnabled = true;
            text_contacto.IsEnabled = true;
            text_telefono.IsEnabled = true;
            button_rgistrar_proveedor.IsEnabled = true;

        }
        private void CargarProveedor()
        {
            VaciarProveedor();
            try
            {
                Proveedor proveedor = new Proveedor();
                proveedor.BuscarProveedor(int.Parse(text_id_proveedor.Text));
                if (proveedor.NombreProveedor == null)
                {
                    ProveedorNoExiste();
                }
                else
                {
                    ProveedorExiste(proveedor);
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        #region ZONA PRODUCTO

        private void button_buscar_producto_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = new Producto();
            ChangeColor();

            VaciarProductos();
            try
            {
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

                    //FinProcesoProductos();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            
            }
            

        }       

        private void button_agregar_datos_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {

                Producto producto = new Producto();
                CrearProducto(producto);
                producto.AgregarProducto();
                HabilitarCamposProductos("Registrado");
               // FinProcesoProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button_actualizar_datos_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = new();
            CrearProducto(producto);
            producto.ActualizarCantidades(producto);
            HabilitarCamposProductos("Actualizado");
          //  FinProcesoProductos();
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
                VaciarProveedor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
                    productos = producto.BuscarPorNombre(text_caja_busqueda.Text + "%");
                    datagrid_busqueda_productos.ItemsSource = productos;
                    break;
                case "Categoria":
                    productos = producto.BuscarPorCategoria(text_caja_busqueda.Text + "%");
                    datagrid_busqueda_productos.ItemsSource = productos;
                    break;
            }


        }

        private void button_listar_Click(object sender, RoutedEventArgs e)
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = new();
            productos = producto.ListarInventario(textbox_listar.Text);
            datagrid_listar.ItemsSource = productos;
        }
        #endregion

        #region PLACEHOLDERS

        private void textbox_listar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox_listar.Text))
            {
                placeholder_listar.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_listar.Visibility = Visibility.Collapsed;
            }
        }

        private void text_caja_busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_caja_busqueda.Text))
            {
                placeholder_caja_busqueda.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_caja_busqueda.Visibility = Visibility.Collapsed;
            }
        }

        private void text_id_proveedor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_id_proveedor.Text))
            {
                placeholder_id_proveedor.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_id_proveedor.Visibility = Visibility.Collapsed;
            }
        }

        private void text_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_nombre.Text))
            {
                placeholder_nombre.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_nombre.Visibility = Visibility.Collapsed;
            }
        }

        private void text_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_telefono.Text))
            {
                placeholder_telefono.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_telefono.Visibility = Visibility.Collapsed;
            }
        }

        private void text_contacto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_contacto.Text))
            {
                placeholder_contacto.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_contacto.Visibility = Visibility.Collapsed;
            }
        }

        private void text_id_producto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_id_producto.Text))
            {
                placeholder_id_producto.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_id_producto.Visibility = Visibility.Collapsed;
            }
        }

        private void text_nombre_producto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_nombre_producto.Text))
            {
                placeholder_nombre_producto.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_nombre_producto.Visibility = Visibility.Collapsed;
            }
        }

        private void text_categoria_producto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_categoria_producto.Text))
            {
                placeholder_categoria_producto.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_categoria_producto.Visibility = Visibility.Collapsed;
            }

        }

        private void text_precio_producto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_precio_producto.Text))
            {
                placeholder_precio_producto.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_precio_producto.Visibility = Visibility.Collapsed;
            }

        }

        private void text_cantidad_producto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_cantidad_producto.Text))
            {
                placeholder_cantidad_producto.Visibility = Visibility.Visible;

            }
            else
            {
                placeholder_cantidad_producto.Visibility = Visibility.Collapsed;
            }
        }
    }


    #endregion

}
