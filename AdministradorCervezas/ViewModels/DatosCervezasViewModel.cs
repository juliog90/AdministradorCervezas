using Caliburn.Micro;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            set
            {
                _cervezas = value;
                NotifyOfPropertyChange(() => Cervezas);
            }
        }

        public Beer CervezaSeleccionada
        {
            get { return _cervezaSeleccionada; }
            set
            {
                _cervezaSeleccionada = value;
                NotifyOfPropertyChange(() => CervezaSeleccionada);
                NotifyOfPropertyChange(() => PuedeEditarBorrar);

            }
        }

        public void Agregar()
        {
            AgregarCervezaViewModel administrarCervezas = new AgregarCervezaViewModel();
            IWindowManager manejador1 = new WindowManager();
            manejador1.ShowDialog(administrarCervezas, null, null);
            Cervezas = null;
            Cervezas = new BindableCollection<Beer>(Beer.GetAll());
        }

        public void Editar()
        {
            EditarCervezaViewModel editarCervezas = new EditarCervezaViewModel(CervezaSeleccionada);
            IWindowManager manejador2 = new WindowManager();
            manejador2.ShowDialog(editarCervezas, null, null);
            Cervezas = null;
            Cervezas = new BindableCollection<Beer>(Beer.GetAll());
        }

        public bool PuedeEditarBorrar
        {
            get
            {
                return CervezaSeleccionada != null;
            }
        }

        public void Borrar()
        {
            MessageBoxResult resultado = MessageBox.Show("Estas seguro de eliminar el elemento?", "Eliminando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                if (CervezaSeleccionada.Delete())
                {
                    Cervezas.Remove(CervezaSeleccionada);
                }
            }

        }
    }
}
