using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministradorCervezas.ViewModels
{
    class DatosMarcasViewModel : Screen
    {
        private BindableCollection<Brand> _marcas = new BindableCollection<Brand>(Brand.GetAll());

        private Brand _marcaSeleccionada;

        public BindableCollection<Brand> Marcas
        {
            get { return _marcas; }
            set
            {
                _marcas = value;
                NotifyOfPropertyChange(() => Marcas);
            }
        }

        public Brand MarcaSeleccionada
        {
            get { return _marcaSeleccionada; }
            set
            {
                _marcaSeleccionada = value;
                NotifyOfPropertyChange(() => MarcaSeleccionada);
                NotifyOfPropertyChange(() => PuedeEditarBorrar);
            }
        }

        public void Agregar()
        {
            AdministrarModeloViewModel administrarMarcas = new AdministrarModeloViewModel();
            IWindowManager manejador1 = new WindowManager();
            manejador1.ShowDialog(administrarMarcas, null, null);
            Marcas = null;
            Marcas = new BindableCollection<Brand>(Brand.GetAll());
        }

        public void Editar()
        {
            EditarMarcasViewModel editarMarcas = new EditarMarcasViewModel(MarcaSeleccionada);
            IWindowManager manejador2 = new WindowManager();
            manejador2.ShowDialog(editarMarcas, null, null);
            Marcas = null;
            Marcas = new BindableCollection<Brand>(Brand.GetAll());
        }

        public bool PuedeEditarBorrar
        {
            get
            {
                return MarcaSeleccionada != null;
            }
        }

        public void Borrar()
        {
            MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de eliminar el elemento?", "Eliminando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MarcaSeleccionada.Delete();
                Marcas = null;
                Marcas = new BindableCollection<Brand>(Brand.GetAll());
                NotifyOfPropertyChange(() => Marcas);
            }

        }

    }
}
