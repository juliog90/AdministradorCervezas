using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    public class DatosModelosViewModel : Screen
    {
        private BindableCollection<Clasification> _clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());

        public BindableCollection<Clasification> Clasificaciones
        {
            get
            {
                return _clasificaciones;
            }
            set
            {
                _clasificaciones = value;
            }
        }

        private Clasification _clasificacionSeleccionada;

        public Clasification ClasificacionSeleccionada
        {
            get { return _clasificacionSeleccionada; }
            set
            {
                _clasificacionSeleccionada = value;
                NotifyOfPropertyChange(() => ClasificacionSeleccionada);
            }
        }

        private BeerType _tipoCervezaSeleccionado;

        public BeerType TipoCervezaSeleccionado
        {
            get { return _tipoCervezaSeleccionado; }
            set
            {
                _tipoCervezaSeleccionado = value;
                NotifyOfPropertyChange(() => TipoCervezaSeleccionado);
            }
        }





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





    }
}
