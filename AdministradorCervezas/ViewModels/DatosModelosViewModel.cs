using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void Agregar()
        {
            AdministrarModeloViewModel administrarMarcas = new AdministrarModeloViewModel();
            IWindowManager manejador1 = new WindowManager();
            manejador1.ShowDialog(administrarMarcas, null, null);
            Clasificaciones = null;
            Clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
        }

        public void Editar()
        {
            //EditarMarcasViewModel editarMarcas = new EditarMarcasViewModel(ClasificacionSeleccionada);
            //IWindowManager manejador2 = new WindowManager();
            //manejador2.ShowDialog(editarMarcas, null, null);
            //Clasificaciones = null;
            //Clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
        }

        public bool PuedeEditarBorrar
        {
            get
            {
                return ClasificacionSeleccionada != null;
            }
        }

        public void Borrar()
        {
            MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de eliminar el elemento?", "Eliminando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                ClasificacionSeleccionada.Delete();
                Clasificaciones = null;
                Clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
                NotifyOfPropertyChange(() => Clasificaciones);
            }

        }



    }
}
