using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace trabajoFinalInterfaces
{
    public partial class Main : Window
    {
        private List<Button> navButtons;
        private DispatcherTimer timerVerificacion;
        private bool ModuloSupervisionActivo;
        public static Main Instancia { get; private set; }


        public Main()
        {
            InitializeComponent();
            Instancia = this; // ← Aquí se guarda la instancia en la propiedad estática

            ModuloSupervisionActivo = false;
            MainFrame.Navigate(new Pedidos()); // Inicio es la página predeterminada (el select*)
            navButtons = new List<Button>
    {
        BtnBorrar, // Nuevas Convos
        BtnAgregar, // Formato MKP
        BtnModificar, // Importar MKP
        BtnPedidos,
        BtnIncidencias,
        BtnProductos,
        BtnInicio,
        BtnDashboard,
        BtnActividad
    };

            //MainFrame.Navigate(new Inicio()); // Inicio es la página predeterminada (el select*)

            // Inicializar el DispatcherTimer
            timerVerificacion = new DispatcherTimer();
            timerVerificacion.Interval = TimeSpan.FromMinutes(2); // Establecer el intervalo a 2 minutos
            timerVerificacion.Tick += TimerVerificacion_Tick; // Asignar el evento Tick
            timerVerificacion.Start(); // Iniciar el timer
            ActualizarUltimaActividad();
            CargarDatosGraficos();
            




        }
        public static void ActualizarUltimaActividad()
        {
            if (Instancia != null)
            {
                Instancia.BtnActividad.ToolTip ="Última actividad: " +BaseDeDatos.ObtenerUltimaActividad();
            }
        }


        public static void CargarDatosGraficos()
        {

            BaseDeDatos.CargarDatosGraficas();




        }

        private void TimerVerificacion_Tick(object sender, EventArgs e)
        {
            // Llamar al método VerificarConexion de la clase BaseDeDatos
            bool conexionActiva = BaseDeDatos.VerificarConexion();

            // Aquí puedes agregar lógica adicional basada en el resultado de la verificación
            if (conexionActiva)
            {
                Console.WriteLine("La conexión a la base de datos está activa.");
                // Podrías actualizar la interfaz de usuario si es necesario
            }
            else
            {
                Console.WriteLine("¡La conexión a la base de datos no está activa!");

                MessageBox.Show(
                    "La conexión a la base de datos ha fallado. La aplicación se cerrará.",
                    "Error de conexión",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                BaseDeDatos.Desconectar(); // Cierra la conexión de forma limpia

                Application.Current.Shutdown(); // Cierra correctamente la aplicación WPF
            }

        }
        private void ResetButtonStyles()
        {
            foreach (var btn in navButtons)
            {
                btn.Style = (Style)FindResource("HubPrincipalButtonStyle1");
            }
        }

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Inicio());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");
        }

        public void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Agregar());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");
        }
        public static void IrAAgregar()
        {
            if (Instancia != null)
            {
                Instancia.MainFrame.Navigate(new Agregar());
                Instancia.ResetButtonStyles();
                Instancia.BtnAgregar.Style = (Style)Instancia.FindResource("HubPrincipalButtonStyle3");
            }
        } 
        public static void IrAModificar()
        {
            if (Instancia != null)
            {
                Instancia.MainFrame.Navigate(new Modificar());
                Instancia.ResetButtonStyles();
                Instancia.BtnModificar.Style = (Style)Instancia.FindResource("HubPrincipalButtonStyle3");
            }
        }


        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Modificar());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {

                MainFrame.Navigate(new Borrar());


            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");
        }


        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pedidos());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");

        }
        private void BtnIncidencias_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Incidencias());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");

        }
        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Productos());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Dashboard());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            BaseDeDatos.Desconectar();
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ModuloSupervision(object sender, RoutedEventArgs e)
        {
            if (ModuloSupervisionActivo == false)
            {
                BtnBorrar.Content = "Dashboard";
                ModuloSupervisionActivo = true;
            }
            else
            {
                BtnBorrar.Content = "Nuevas Convos";
                ModuloSupervisionActivo = false;
            }

        }

        private void BtnActividad_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Actividad());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");


           // BtnActividad.ToolTip = "Adiós";
        }
  

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Reportes());
            ResetButtonStyles();
            ((Button)sender).Style = (Style)FindResource("HubPrincipalButtonStyle3");
        }
    }
}