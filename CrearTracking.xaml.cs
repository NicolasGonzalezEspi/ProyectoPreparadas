using System.Windows;

namespace trabajoFinalInterfaces
{
    public partial class CrearTracking : Window
    {
        public CrearTracking()
        {
            InitializeComponent();
            idPedido.Text = Pedidos.trackings[0];

            txtFecha_gestion2.Text = "Gestión: " + GetFormattedDate(Pedidos.trackings[1]);
            txtTracking.Text = Pedidos.trackings[2];
            txtFecha_inicio_transito.Text = GetFormattedDate(Pedidos.trackings[3]);
            txtFecha_entrega_alumna.Text = GetFormattedDate(Pedidos.trackings[4]);
        }

        private string GetFormattedDate(string dateString)
        {
            return dateString.Length >= 10 ? dateString.Substring(0, 10) : dateString;
        }

        private void CrearDatosTracking(object sender, RoutedEventArgs e)
        {
            string consulta = "";
            if (string.IsNullOrWhiteSpace(txtTracking.Text) && string.IsNullOrWhiteSpace(txtFecha_inicio_transito.Text) && string.IsNullOrWhiteSpace(txtFecha_entrega_alumna.Text))
            {
                //si no hay nada incluido 
                consulta = @"
                UPDATE pedidos 
                SET 
                     estado_pedido='Gestionado',
                    tracking = @tracking, 
                    fecha_inicio_transito = @fecha_inicio_transito, 
                    fecha_entrega_alumna = @fecha_entrega_alumna 
                WHERE id_pedido_mk = @id_pedido_mk;";
            }
            else if ((!string.IsNullOrWhiteSpace(txtTracking.Text)  || !string.IsNullOrWhiteSpace(txtFecha_inicio_transito.Text)) && string.IsNullOrWhiteSpace(txtFecha_entrega_alumna.Text))
            {
                //si hay tracking o fecha inicio tránsito y no hay fecha de entrega 
                consulta = @"
                UPDATE pedidos 
                SET 
                    estado_pedido='En tránsito',
                    tracking = @tracking, 
                    fecha_inicio_transito = @fecha_inicio_transito, 
                    fecha_entrega_alumna = @fecha_entrega_alumna 
                WHERE id_pedido_mk = @id_pedido_mk;";
            }
            else if (!string.IsNullOrWhiteSpace(txtFecha_entrega_alumna.Text))
            {
                //si hay fecha de entrega
                consulta = @"
                UPDATE pedidos 
                SET 
                    estado_pedido='Entregado',
                    tracking = @tracking, 
                    fecha_inicio_transito = @fecha_inicio_transito, 
                    fecha_entrega_alumna = @fecha_entrega_alumna 
                WHERE id_pedido_mk = @id_pedido_mk;";
            }

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {

                { "@tracking", string.IsNullOrWhiteSpace(txtTracking.Text) ? DBNull.Value : (object)txtTracking.Text },
                { "@fecha_inicio_transito", string.IsNullOrWhiteSpace(txtFecha_inicio_transito.Text) ? DBNull.Value : (object)FormatearFecha(txtFecha_inicio_transito.Text) },
                { "@fecha_entrega_alumna", string.IsNullOrWhiteSpace(txtFecha_entrega_alumna.Text) ? DBNull.Value : (object)FormatearFecha(txtFecha_entrega_alumna.Text) },
                { "@id_pedido_mk", idPedido.Text }
            };

            bool resultado = BaseDeDatos.EjecutarQueryIncidencia(consulta, parametros);

            if (resultado)
            {
                MessageBox.Show("Datos del tracking actualizados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public static string FormatearFecha(string fecha)
        {
            //cambiar de d/M/Y a Y/M/d (español a americano)
            try
            {
                string dia = fecha.Substring(0, 2);
                string mes = fecha.Substring(3, 2);
                string anio = fecha.Substring(6, 4);
                return $"{anio}/{mes}/{dia}";
            }
            catch
            {
                return fecha; // En caso de error, retornar como está
            }
        }
    }
}
