using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class DatosOrdenesViewModel : Screen
    {
        private BindableCollection<Order> _ordenes = new BindableCollection<Order>(Order.GetAll());
        private Order _ordenSeleccionada;

        public BindableCollection<Order> Ordenes
        {
            get { return _ordenes; }
            set { _ordenes = value; }
        }

        public Order OrdenSeleccionada
        {
            get { return _ordenSeleccionada; }
            set
            {
                _ordenSeleccionada = value;
                // Le avisa que hubo un cambio
                NotifyOfPropertyChange(() => OrdenSeleccionada);
            }
        }
    }
}
