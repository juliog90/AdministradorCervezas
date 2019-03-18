using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdministradorCervezas.ViewModels
{
    class DatosTiposViewModel: Screen
    {
        private BindableCollection<BeerType> _tipos = new BindableCollection<BeerType>(BeerType.GetAll());    
        private BeerType _tipoSeleccionado;

        public BindableCollection<BeerType> Tipos
        {
            get { return _tipos; }
            set { _tipos = value;
                NotifyOfPropertyChange(() => Tipos);
            }
        }

        public BeerType TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set { _tipoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoSeleccionado);
            }
        }

        public void Agregar()
        {
            AgregarTipoViewModel agregarTipo = new AgregarTipoViewModel();
            IWindowManager manejador = new WindowManager();
            manejador.ShowWindow(agregarTipo, null, null);
            Tipos = null;
            Tipos = new BindableCollection<BeerType>(BeerType.GetAll());
        }
        public void Editar()
        {

        }

        public bool PuedeEditar
        {
            get
            {
                return TipoSeleccionado != null;
            }
        }
    }
}
