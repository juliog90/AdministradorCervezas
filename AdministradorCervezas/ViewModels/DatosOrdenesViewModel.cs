using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    class DatosOrdenesViewModel : Screen
    {
        private BindableCollection<Order> _ordenes = new BindableCollection<Order>(Order.GetAll());

        public BindableCollection<Order> Ordenes
        {
            get { return _ordenes; }
            set { _ordenes = value; }
        }

        private Order _ordenSeleccionada;

        public Order OrdenSeleccionada
        {
            get { return _ordenSeleccionada; }
            set { _ordenSeleccionada = value; }
        }


    }
}
