using Caliburn.Micro;
using System.Windows;

namespace AdministradorCervezas.ViewModels
{
    class EditarClasificacionViewModel : Screen
    {
        private string _nombre;
        private string _codigo;
        private BindableCollection<BeerType> _tipos = new BindableCollection<BeerType>(BeerType.GetAll());
        private BeerType _tipoSeleccionado;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                NotifyOfPropertyChange(() => Nombre);
            }
        }

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                _codigo = value;
                NotifyOfPropertyChange(() => Codigo);
            }
        }

        public BindableCollection<BeerType> Tipos
        {
            get { return _tipos; }
        }

        public BeerType TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set
            {
                _tipoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoSeleccionado);
            }
        }

        public EditarClasificacionViewModel(Clasification seleccion)
        {
            Nombre = seleccion.Name;
            int indice = Tipos.IndexOf(seleccion.BeerType);

            // no me gusta mucho, pero funciona
            // busca el elemento de la lista que sea igual al que escogiste
            // anteriormente y lo pone como seleccionado
            for (int i = 0; i < Tipos.Count; i++)
            {
                if (seleccion.BeerType.Id == Tipos[i].Id)
                {
                    TipoSeleccionado = Tipos[i];
                    break;
                }
            }
            Codigo = seleccion.Code;
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

        public void Editar()
        {
            MessageBoxResult resultado = MessageBox.Show("Estas seguro de editar esta clasificacion?", "Guardando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                Clasification clasificacion = new Clasification();
                clasificacion.Name = Nombre;
                clasificacion.Code = Codigo;
                clasificacion.BeerType = TipoSeleccionado;
                clasificacion.Edit();
            }
        }
    }
}
