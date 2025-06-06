using System;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace trabajoFinalInterfaces
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Page
    {
        public Productos()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            DataTable productos = BaseDeDatos.MostrarTodosProductos();
            dgProductos.ItemsSource = productos.DefaultView;
        }

        private void DarDeAlta(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItems.Count == 0)
            {
                MessageBox.Show("No hay ningún registro seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (dgProductos.SelectedItems.Count > 1)
            {
                MessageBox.Show("Por favor, seleccione solo un registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
            String id_producto = filaSeleccionada["id_producto"].ToString();

            String consulta = $"UPDATE productos SET estado = 'Alta' WHERE id_producto = '{id_producto}';";

            bool exito = BaseDeDatos.EjecutarUpdateIncidencia(consulta);
            if(exito) {
                CargarProductos();
            }
        }

        private void DarDeBaja(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItems.Count == 0)
            {
                MessageBox.Show("No hay ningún registro seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (dgProductos.SelectedItems.Count > 1)
            {
                MessageBox.Show("Por favor, seleccione solo un registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
            String id_producto = filaSeleccionada["id_producto"].ToString();

            String consulta = $"UPDATE productos SET estado = 'Baja' WHERE id_producto = '{id_producto}';";

            bool exito = BaseDeDatos.EjecutarUpdateIncidencia(consulta);
            if (exito)
            {
                CargarProductos();
            }
        }

        private void CrearProductoMKP(object sender, RoutedEventArgs e)
        {
            var ventana = new CrearProducto();
            bool? resultadoVentana = ventana.ShowDialog();

            if(resultadoVentana == true)
            {
                CargarProductos();
            }


        }

    }
}
