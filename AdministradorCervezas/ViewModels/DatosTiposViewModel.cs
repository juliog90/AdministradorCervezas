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
        private BindableCollection<BeerType> _tipos;    

        public BindableCollection<BeerType> Tipos
        {
            get { return _tipos; }
            set { _tipos = value;
                NotifyOfPropertyChange(() => Tipos);
            }

        }

        private BeerType _tipoSeleccionado;

        public BeerType TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set { _tipoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoSeleccionado);
            }
        }

        public void Agregar()
        {

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
