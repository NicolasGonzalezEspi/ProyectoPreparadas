using System.Windows;

namespace trabajoFinalInterfaces
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MostrarUsuarios(object sender, RoutedEventArgs e) //aunque ponga mostrar usuarios, es como el login
        {
            
                Main ventanaPrincipal = new Main();
                ventanaPrincipal.Show();
                this.Close();
           
        }

    }
}
