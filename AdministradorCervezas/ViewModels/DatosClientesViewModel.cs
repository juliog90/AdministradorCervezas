using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    class DatosClientesViewModel : Screen
    {
        private BindableCollection<Customer> _clientes = new BindableCollection<Customer>(Customer.GetAll());

        public BindableCollection<Customer> Clientes
        {
            get { return _clientes; }
            set { _clientes = value; }
        }

        private Customer _clienteSeleccionado;

        public Customer ClienteSeleccionado
        {
            get { return _clienteSeleccionado; }
            set { _clienteSeleccionado = value; }
        }


    }
}
