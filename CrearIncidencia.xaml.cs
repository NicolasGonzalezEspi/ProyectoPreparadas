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
    /// Lógica de interacción para CrearIncidencia.xaml
    /// </summary>
    public partial class CrearIncidencia : Window
    {
        public string IncidenciaTexto { get; private set; }

        public CrearIncidencia(string textoInicial)
        {
            InitializeComponent();
            txtIncidencia.Text = textoInicial;

            // Mover el cursor al final del texto
            txtIncidencia.CaretIndex = txtIncidencia.Text.Length;

            // Poner el foco al TextBox
            txtIncidencia.Focus();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            IncidenciaTexto = txtIncidencia.Text;
            DialogResult = true;
            Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
