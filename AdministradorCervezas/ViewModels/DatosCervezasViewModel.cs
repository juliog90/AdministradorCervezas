using AdministradorCervezas.ViewModels;
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
        // convertimos la lista del modelo a un coleccion mas util para Caliburn
        private BindableCollection<Beer> _cervezas = new BindableCollection<Beer>(Beer.GetAll());

        private Beer _cervezaSeleccionada;

        public BindableCollection<Beer> Cervezas
        {
            get { return _cervezas; }
        }

        public Beer CervezaSeleccionada
        {
            get { return _cervezaSeleccionada; }
            set
            {
                _cervezaSeleccionada = value;
                NotifyOfPropertyChange(() => CervezaSeleccionada);
            }
        }

        public void CambioSeleccion()
        {
            
        }

        public void Agregar()
        {
            AdministrarCervezaModelView administrarCervezas = new AdministrarCervezaModelView();
            IWindowManager manager = new WindowManager();
            manager.ShowDialog(administrarCervezas, null, null);
        }
    }
}
