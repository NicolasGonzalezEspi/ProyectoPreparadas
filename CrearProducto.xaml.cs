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
    /// Lógica de interacción para CrearProducto.xaml
    /// </summary>
    public partial class CrearProducto : Window
    {
        public CrearProducto()
        {
            InitializeComponent();
        }


        private void CrearProductoMKP(object sender, RoutedEventArgs e)
        {
            // Obtener valores desde la interfaz
            string sku = txtSKU.Text.Trim();
            string nombre = txtNombre_Producto.Text.Trim();
            string modelo = txtModelo.Text.Trim();
            string puntosTexto = txtPuntos.Text.Trim();

            // Validaciones básicas
            if (string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(modelo) || string.IsNullOrEmpty(puntosTexto))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(puntosTexto, out int puntos))
            {
                MessageBox.Show("El campo 'Puntos' debe ser un número entero.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear consulta SQL
            string consulta = $@"
        INSERT INTO productos (id_producto, nombre_producto, modelo, puntos, estado)
        VALUES ('{sku}', '{nombre}', '{modelo}', {puntos}, 'Alta')";

            // Ejecutar consulta
            bool resultado = BaseDeDatos.EjecutarUpdateIncidencia(consulta);

            if (resultado)
            {
              //  MessageBox.Show("Producto creado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Opcional: limpiar campos
                txtSKU.Clear();
                txtNombre_Producto.Clear();
                txtModelo.Clear();
                txtPuntos.Clear();
                DialogResult = true;
                Close();
            }
        }

    }
}
