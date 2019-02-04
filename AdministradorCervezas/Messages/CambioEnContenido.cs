using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace AdministradorCervezas.Messages
{
    public class CambioEnContenido
    {
        private IEventAggregator _events;

        public CambioEnContenido(IEventAggregator events)
        {
            _events = events;
        }

        public Beer CervezaActual
        {
            get;
            private set;
        }

        public CambioEnContenido(Beer cervezaActual)
        {
            CervezaActual = cervezaActual;
        }
    }
}
