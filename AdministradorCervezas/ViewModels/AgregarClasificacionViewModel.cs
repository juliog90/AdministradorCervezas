using Caliburn.Micro;
using System.Windows;

namespace AdministradorCervezas.ViewModels
{
    public class AgregarClasificacionViewModel : Screen
    {
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                NotifyOfPropertyChange(() => Nombre);
            }
        }

        private string _codigo;

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                _codigo = value;
                NotifyOfPropertyChange(() => Codigo);
            }
        }

        private BindableCollection<BeerType> _tipos = new BindableCollection<BeerType>(BeerType.GetAll());

        public BindableCollection<BeerType> Tipos
        {
            get { return _tipos; }
            set { _tipos = value; }
        }

        private BeerType _tipoSeleccionado;

        public BeerType TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set
            {
                _tipoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoSeleccionado);
            }
        }

        public bool PuedesEscribirNombre
        {
            get
            {
                return TipoSeleccionado != null;
            }
        }

        public bool PuedesEscibirCodigo
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Nombre);
            }
        }

        public void Guardar()
        {
            MessageBoxResult resultado = MessageBox.Show("Estas seguro de guardar esta clasificacion?", "Guardando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                Clasification clasificacion = new Clasification();
                clasificacion.Name = Nombre;
                clasificacion.Code = Codigo;
                clasificacion.BeerType = TipoSeleccionado;
                clasificacion.Add();
                Reiniciar();
            }
        }

        public void Reiniciar()
        {
            Nombre = "";
            Codigo = "";
            TipoSeleccionado = null;
        }
    }
}
