using AdministradorCervezas.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private ImageSource _cervezaImagen; 


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

        public ImageSource CervezaImagen
        {
            get
            {
                return _cervezaImagen;
            }
            set
            {
                _cervezaImagen = value;
                NotifyOfPropertyChange(() => CervezaImagen);
                CargaImagen();
            }
        }

        public void Agregar()
        {
            AdministrarCervezaViewModel administrarCervezas = new AdministrarCervezaViewModel(Cervezas);
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

        public void CargaImagen()
        {
            CervezaImagen = new BitmapImage(new Uri("http://localhost/the_brewery/images/" + CervezaSeleccionada.Image, UriKind.Absolute));
        }

        public void Borrar()
        {
            MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de eliminar el elemento?", "Eliminando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                CervezaSeleccionada.Delete();
                Cervezas = null;
                Cervezas = new BindableCollection<Beer>(Beer.GetAll());
            }
            
        }
    }
}
