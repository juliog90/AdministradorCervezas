using Caliburn.Micro;


namespace AdministradorCervezas.ViewModels
{
    class DatosTiposViewModel : Screen
    {
        private BindableCollection<BeerType> _tipos = new BindableCollection<BeerType>(BeerType.GetAll());
        private BeerType _tipoSeleccionado;
        private IWindowManager manejador = new WindowManager();

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
            manejador.ShowWindow(agregarTipo, null, null);
            NotifyOfPropertyChange(() => Tipos);
        }

        public void Editar()
        {
            EditarTipoViewModel editarTipo = new EditarTipoViewModel(TipoSeleccionado);
            manejador.ShowWindow(editarTipo, null, null);
            NotifyOfPropertyChange(() => Tipos);
        }

        public void Borrar()
        {
            if(TipoSeleccionado.Delete())
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
