using System.Windows;

namespace AdministradorCervezas.Views
{
    /// <summary>
    /// Interaction logic for PrincipalView.xaml
    /// </summary>
    public partial class PrincipalView : Window
    {
        public PrincipalView()
        {
            InitializeComponent();
        }


        private void CargaCervezas_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Brewery - Administrando Cervezas";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de salir del sistema?", "Saliendo", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                this.Close();
            }


        }

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
