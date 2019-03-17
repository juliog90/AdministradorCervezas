using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class AgregarMarcaViewModel : Screen
    {
        private BindableCollection<Country> _paises = new BindableCollection<Country>(Country.GetAll());

        public BindableCollection<Country> Paises
        {
            get { return _paises; }
            set { _paises = value; }
        }

        private string _nombreMarca;

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

        private string _marcaCodigo;

        public string MarcaCodigo
        {
            get { return _marcaCodigo; }
            set
            {
                _marcaCodigo = value;
                NotifyOfPropertyChange(() => MarcaCodigo);
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => PuedeGuardar);
            }
        }


        private Country _paisSeleccionado;

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
                return PaisSeleccionado != null && !string.IsNullOrWhiteSpace(NombreMarca) && !string.IsNullOrWhiteSpace(MarcaCodigo);
            }
        }

        public bool PuedesEscogerPais
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreMarca);
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
