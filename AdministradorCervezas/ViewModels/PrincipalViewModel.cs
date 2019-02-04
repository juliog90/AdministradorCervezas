using AdministradorCervezas.Messages;
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
    class PrincipalViewModel : Conductor<object>, IHandle<CambioEnContenido>
    {

        private Beer _cervezaActual;
        private IEventAggregator _events;

        public DatosCervezasViewModel datosCerveza
        {
            get;
            set;
        }

        public PrincipalViewModel()
        {
            IEventAggregator events = new EventAggregator();
            datosCerveza = new DatosCervezasViewModel(events);
        }

        public PrincipalViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
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

        public void Editar()
        {

        }

        public void CanEditar()
        {
            return ;
        }

        public void Handle(CambioEnContenido message)
        {
            _cervezaActual = (Beer)message;
        }
    }
}
