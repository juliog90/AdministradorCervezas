using AdministradorCervezas.ViewModels;
using AdministradorCervezas.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    class PrincipalViewModel : Conductor<object>
    {
        private Beer _cervezaActual;

        public Beer CervezaActual
        {
            get { return _cervezaActual; }
            set
            {
                _cervezaActual = value;
                NotifyOfPropertyChange(() => CervezaActual);
            }
        }

        public void CargaCervezas()
        {
            ActivateItem(new DatosCervezasViewModel());
        }

        public void CargaMarcas()
        {
            ActivateItem(new DatosMarcasViewModel());
        }

        public void CargaClientes()
        {
            ActivateItem(new DatosClientesViewModel());
        }

        public void CargaOrdenes()
        {
            ActivateItem(new DatosOrdenesViewModel());
        }
    }
}
