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
using System.Data;

namespace trabajoFinalInterfaces
{
    /// <summary>
    /// Lógica de interacción para Actividad.xaml
    /// </summary>
    public partial class Actividad : Page
    {
        public Actividad()
        {
            InitializeComponent();
            CargarActividadPuntos();
        }
        private void CargarActividadPuntos()
        {
            DataTable puntos = BaseDeDatos.MostrarActividadProductos();
            dgPuntos.ItemsSource = puntos.DefaultView;
        }
    }
}
