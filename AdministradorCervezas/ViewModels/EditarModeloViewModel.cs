using Caliburn.Micro;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace AdministradorCervezas.ViewModels
{
    class EditarModeloViewModel : Screen
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

        public EditarModeloViewModel(Clasification seleccion)
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
