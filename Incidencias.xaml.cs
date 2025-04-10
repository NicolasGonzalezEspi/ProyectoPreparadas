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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace trabajoFinalInterfaces
{
    /// <summary>
    /// Lógica de interacción para Incidencias.xaml
    /// </summary>
    public partial class Incidencias : Page
    {
        public Incidencias()
        {
            InitializeComponent();
            CargarIncidencias();
        }

        private void CargarIncidencias()
        {
            DataTable productos = BaseDeDatos.MostrarIncidencias();
            dgProductos.ItemsSource = productos.DefaultView;
        }

        private void Actualizar_Incidencia(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItems.Count == 1) // Ahora verificamos que haya exactamente una fila seleccionada
            {
                string incidencia = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese la incidencia:",
                    "Añadir Incidencia",
                    ""
                );

                if (!string.IsNullOrWhiteSpace(incidencia))
                {
                    DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem; // Accedemos directamente al único elemento seleccionado

                    string idPedido = filaSeleccionada["id_pedido_mk_i"].ToString();

                    bool actualizado = BaseDeDatos.ActualizarIncidencia(idPedido, incidencia);

                    if (actualizado)
                    {
                        MessageBox.Show("Incidencia añadida correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo añadir la incidencia.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    CargarIncidencias(); // Refrescar los datos de la tabla
                }
            }
            else if (dgProductos.SelectedItems.Count > 1) // Informamos si hay más de un elemento seleccionado
            {
                MessageBox.Show("Seleccione solo un elemento para añadir la incidencia.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else // Informamos si no hay ningún elemento seleccionado
            {
                MessageBox.Show("Seleccione un elemento.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
