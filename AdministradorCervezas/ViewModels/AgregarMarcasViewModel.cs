using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class AgregarMarcaViewModel : Screen
    {
        private BindableCollection<Country> _paises = new BindableCollection<Country>(Country.GetAll());
        private string _nombreMarca = "";
        private string _marcaCodigo = "";
        private string _name;
        private Country _paisSeleccionado;

        public BindableCollection<Country> Paises
        {
            get { return _paises; }
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

        public string MarcaCodigo
        {
            get { return _marcaCodigo; }
            set
            {
                _marcaCodigo = value;
                NotifyOfPropertyChange(() => MarcaCodigo);
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
            }
        }

        public Country PaisSeleccionado
        {
            get { return _paisSeleccionado; }
            set
            {
                _paisSeleccionado = value;
                NotifyOfPropertyChange(() => PaisSeleccionado);
                NotifyOfPropertyChange(() => PuedeGuardar);
            }
        }

        public bool PuedeGuardar
        {
            get
            {
                return PaisSeleccionado != null;
            }
        }

        public bool PuedesEscogerPais
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreMarca) && !string.IsNullOrWhiteSpace(MarcaCodigo);
            }
        }

        public void Guardar()
        {
            Brand nuevaMarca = new Brand();
            nuevaMarca.Id = _marcaCodigo;
            nuevaMarca.Name = NombreMarca;
            nuevaMarca.Country = PaisSeleccionado;
            nuevaMarca.Add();
            Reiniciar();
        }

        public void Reiniciar()
        {
            NombreMarca = "";
            MarcaCodigo = "";
            PaisSeleccionado = null;
        }
    }
}
