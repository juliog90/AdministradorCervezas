﻿using Caliburn.Micro;
using System.Windows;

namespace AdministradorCervezas.ViewModels
{
    public class DatosClasificacionesViewModel : Screen
    {
        // convertimos la lista del modelo a un coleccion mas util para Caliburn
        private BindableCollection<Clasification> _clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
        private Clasification _clasificacionSeleccionada;

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

        public Clasification ClasificacionSeleccionada
        {
            get { return _clasificacionSeleccionada; }
            set
            {
                _clasificacionSeleccionada = value;
                NotifyOfPropertyChange(() => ClasificacionSeleccionada);
                NotifyOfPropertyChange(() => PuedeEditarBorrar);
            }
        }

        public void Agregar()
        {
            AgregarClasificacionViewModel agregarClasificacion = new AgregarClasificacionViewModel();
            IWindowManager manejador1 = new WindowManager();
            manejador1.ShowDialog(agregarClasificacion, null, null);
            Clasificaciones = null;
            Clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
            NotifyOfPropertyChange(() => Clasificaciones);

        }

        public void Editar()
        {
            EditarClasificacionViewModel editarClasicacion = new EditarClasificacionViewModel(ClasificacionSeleccionada);
            IWindowManager manejador2 = new WindowManager();
            manejador2.ShowDialog(editarClasicacion, null, null);
            Clasificaciones = null;
            Clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
            NotifyOfPropertyChange(() => Clasificaciones);
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

        public bool PuedeEditarBorrar
        {
            get
            {
                return ClasificacionSeleccionada != null;
            }
        }
    }
}
