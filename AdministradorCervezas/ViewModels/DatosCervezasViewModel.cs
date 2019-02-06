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
                NotifyOfPropertyChange(() => PuedeEditar);
            }
        }

        public void CambioSeleccion()
        {
            
        }

        public void Agregar()
        {
            AdministrarCervezaViewModel administrarCervezas = new AdministrarCervezaViewModel();
            IWindowManager manejador1 = new WindowManager();
            manejador1.ShowDialog(administrarCervezas, null, null);
        }

        public void Editar()
        {
            EditarCervezaViewModel editarCervezas = new EditarCervezaViewModel(CervezaSeleccionada);
            IWindowManager manejador2 = new WindowManager();
            manejador2.ShowDialog(editarCervezas, null, null);
        }

        public bool PuedeEditar
        {
            get
            {
                return CervezaSeleccionada != null;
            }
        }
    }
}
