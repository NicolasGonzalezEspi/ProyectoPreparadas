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
    /// Lógica de interacción para CrearFactura.xaml
    /// </summary>
    public partial class CrearFactura : Window
    {
        public CrearFactura()
        {
            InitializeComponent();

            idPedido.Text = Pedidos.facturas[0];

            txtFactura.Text = Pedidos.facturas[1];

            txtfechaFactura.Text = Pedidos.facturas[2];

            txtFacturaSinIVA.Text = Pedidos.facturas[3];


        }
        private static string QuitarSimboloEuroFinal(string texto)
        {
            if (!string.IsNullOrEmpty(texto) && texto.EndsWith("€"))
            {
                return texto.Substring(0, texto.Length - 1);
            }
            return texto;
        }
        private void CrearDatosFactura(object sender, RoutedEventArgs e)
        {
            String facturaSinIVA = txtFacturaSinIVA.Text;
            facturaSinIVA= facturaSinIVA.Replace(',', '.');

            facturaSinIVA = QuitarSimboloEuroFinal(facturaSinIVA);


            txtFacturaSinIVA.Text = facturaSinIVA;

            String consulta = @"
                UPDATE pedidos 
                SET 
                    facturadmi_snlosllanos= @facturadmi_snlosllanos, 
                    fecha_factura = @fecha_factura, 
                    factura_sin_iva = @factura_sin_iva
                WHERE id_pedido_mk = @id_pedido_mk;";


            Dictionary<string, object> parametros = new Dictionary<string, object>
            {

                { "@facturadmi_snlosllanos", string.IsNullOrWhiteSpace(txtFactura.Text) ? DBNull.Value : (object)txtFactura.Text },
                { "@fecha_factura", string.IsNullOrWhiteSpace(txtfechaFactura.Text) ? DBNull.Value : (object)CrearTracking.FormatearFecha(txtfechaFactura.Text) },
                { "@factura_sin_iva", string.IsNullOrWhiteSpace(txtFacturaSinIVA.Text) ? DBNull.Value : (object)txtFacturaSinIVA.Text },
                { "@id_pedido_mk", idPedido.Text }
            };


            bool resultado = BaseDeDatos.EjecutarQueryIncidencia(consulta, parametros);

            if (resultado)
            {
                MessageBox.Show("Datos de factura actualizados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            else
            {
                //  MessageBox.Show("No se pudo actualizar el tracking.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //  DialogResult = false;
                // Close();
            }

        }
    }
}
