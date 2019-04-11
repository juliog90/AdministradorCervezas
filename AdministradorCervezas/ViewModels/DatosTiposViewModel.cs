using Caliburn.Micro;


namespace AdministradorCervezas.ViewModels
{
    public class DatosTiposViewModel : Screen
    {
        private BindableCollection<BeerType> _tipos = new BindableCollection<BeerType>(BeerType.GetAll());
        private BeerType _tipoSeleccionado;

        public BindableCollection<BeerType> Tipos
        {
            get { return _tipos; }
            set
            {
                _tipos = value;
                NotifyOfPropertyChange(() => Tipos);
            }
        }

        public BeerType TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set
            {
                _tipoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoSeleccionado);
                NotifyOfPropertyChange(() => PuedeEditarBorrar);
            }
        }

        public void Agregar()
        {
            AgregarTipoViewModel agregarTipo = new AgregarTipoViewModel();
            IWindowManager manejador = new WindowManager();
            manejador.ShowDialog(agregarTipo, null, null);
            Tipos = null;
            Tipos = new BindableCollection<BeerType>(BeerType.GetAll());
        }

        public void Editar()
        {
            EditarTipoViewModel editarTipo = new EditarTipoViewModel(TipoSeleccionado);
            IWindowManager manejador2 = new WindowManager();
            manejador2.ShowDialog(editarTipo, null, null);
            Tipos = null;
            Tipos = new BindableCollection<BeerType>(BeerType.GetAll());
        }

        public void Borrar()
        {
            if (TipoSeleccionado.Delete())
            {
                Tipos.Remove(TipoSeleccionado);
            }
        }

        public bool PuedeEditarBorrar
        {
            get
            {
                return TipoSeleccionado != null;
            }
        }
    }
}
