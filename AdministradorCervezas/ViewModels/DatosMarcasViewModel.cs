using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    class DatosMarcasViewModel : Screen
    {
        private BindableCollection<Brand> _marcas;

        private Brand _marcaSeleccionada;

        public BindableCollection<Brand> Marcas
        {
            get { return _marcas; }
            set { _marcas = value; }
        }

        public Brand MarcaSeleccionada
        {
            get { return _marcaSeleccionada; }
            set { _marcaSeleccionada = value; }
        }


    }
}
