using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    public class DatosCervezasViewModel : Screen
    {
        private IEventAggregator _events;

        // convertimos la lista del modelo a un coleccion mas util para Caliburn
        private BindableCollection<Beer> _cervezas = new BindableCollection<Beer>(Beer.GetAll());

        private Beer _cervezaSeleccionada;

        public DatosCervezasViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
        }

        public DatosCervezasViewModel()
        {
            
        }

        public BindableCollection<Beer> Cervezas
        {
            get { return _cervezas; }
        }

        public Beer CervezaSeleccionada
        {
            get { return _cervezaSeleccionada; }
            set { _cervezaSeleccionada = value; }
        }

        public BindableCollection<Beer> Cervezas

    }
}
