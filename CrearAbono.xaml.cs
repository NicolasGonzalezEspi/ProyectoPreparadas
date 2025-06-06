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

namespace trabajoFinalInterfaces
{
    /// <summary>
    /// Lógica de interacción para CrearAbono.xaml
    /// </summary>
    public partial class CrearAbono : Window
    {
        public CrearAbono()
        {
            InitializeComponent();
            idPedido.Text = Pedidos.abonos[0];
            txtAbono.Text = Pedidos.abonos[1];
            try
            {
                txtFechaAbono.Text = Pedidos.abonos[2].Substring(0, 10);
               
            } catch
            {
                txtFechaAbono.Text = Pedidos.abonos[2];
            }
            txtAbonoSinIVA.Text = Pedidos.abonos[3];
        }


        private void CrearDatosAbono(object sender, RoutedEventArgs e)
        {
            string consulta = "UPDATE pedidos SET fecha_abono = @fecha_abono, abono_sin_iva = @abono_sin_iva, abono = @abono WHERE id_pedido_mk = @id_pedido_mk;";

            Dictionary<string, object> parametros = new Dictionary<string, object>
    {
        { "@fecha_abono", string.IsNullOrWhiteSpace(txtFechaAbono.Text) ? DBNull.Value : (object)FormatearFecha(txtFechaAbono.Text) },
        { "@abono_sin_iva", string.IsNullOrWhiteSpace(txtAbonoSinIVA.Text) ? DBNull.Value : (object)txtAbonoSinIVA.Text.Replace(',', '.') },
        { "@abono", string.IsNullOrWhiteSpace(txtAbono.Text) ? DBNull.Value : (object)txtAbono.Text },
        { "@id_pedido_mk", idPedido.Text }
    };

            bool resultado = BaseDeDatos.EjecutarQueryIncidencia(consulta, parametros);

            if (resultado)
            {
                MessageBox.Show("Datos del abono actualizados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar los datos del abono.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
                Close();
            }
        }



        public static String FormatearFecha(String fechaTipo1)
        {
            string dia = fechaTipo1.Substring(0, 2);
            string mes = fechaTipo1.Substring(3, 2);
            string anio = fechaTipo1.Substring(6, 4);

            return $"{anio}/{mes}/{dia}";
        }

        private void txtFechaAbono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
