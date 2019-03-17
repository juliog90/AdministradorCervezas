using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class EditarMarcasViewModel : Screen
    {
        private BindableCollection<Country> _paises = new BindableCollection<Country>(Country.GetAll());
        private string _nombreMarca;
        private string _marcaCodigo;
        private string _name;
        private Country _paisSeleccionado;

        public BindableCollection<Country> Paises
        {
            get { return _paises; }
            set { _paises = value; }
        }

        public EditarMarcasViewModel(Brand editarMarca)
        {
            NombreMarca = editarMarca.Name;
            _marcaCodigo = editarMarca.Id;

            for (int i = 0; i < Paises.Count; i++)
            {
                if (editarMarca.Country.Id == Paises[i].Id)
                {
                    PaisSeleccionado = Paises[i];
                    break;
                }
            }
        }

        public string NombreMarca
        {
            get { return _nombreMarca; }
            set
            {
                _nombreMarca = value;
                NotifyOfPropertyChange(() => NombreMarca);
                NotifyOfPropertyChange(() => PuedesEscogerPais);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => PuedeEditar);
            }
        }

        public Country PaisSeleccionado
        {
            get { return _paisSeleccionado; }
            set
            {
                _paisSeleccionado = value;
                NotifyOfPropertyChange(() => PaisSeleccionado);
                NotifyOfPropertyChange(() => PuedeEditar);
            }
        }

        public bool PuedeEditar
        {
            get
            {
                return PaisSeleccionado != null && !string.IsNullOrWhiteSpace(NombreMarca) && !string.IsNullOrWhiteSpace(_marcaCodigo);
            }
        }

        public bool PuedesEscogerPais
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreMarca);
            }
        }

        public void Editar()
        {
            Brand nuevaMarca = new Brand();
            nuevaMarca.Id = _marcaCodigo;
            nuevaMarca.Name = NombreMarca;
            nuevaMarca.Country = PaisSeleccionado;
            nuevaMarca.Edit();
        }
    }
}
