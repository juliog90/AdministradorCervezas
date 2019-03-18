using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class DatosClientesViewModel : Screen
    {
        private BindableCollection<Customer> _clientes = new BindableCollection<Customer>(Customer.GetAll());
        private Customer _clienteSeleccionado;

        public BindableCollection<Customer> Clientes
        {
            get { return _clientes; }
            set { _clientes = value; }
        }

        public Customer ClienteSeleccionado
        {
            get { return _clienteSeleccionado; }
            set { _clienteSeleccionado = value; }
        }
    }
}
