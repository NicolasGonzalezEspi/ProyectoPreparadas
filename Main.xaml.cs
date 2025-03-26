using System.Windows;
using System.Windows.Controls;

namespace trabajoFinalInterfaces
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            MainFrame.Navigate(new Inicio()); // Inicio es la página predeterminada (el select*)
        }

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Inicio());
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Agregar());
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Modificar());
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Borrar());
        }
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            BaseDeDatos.Desconectar();
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}