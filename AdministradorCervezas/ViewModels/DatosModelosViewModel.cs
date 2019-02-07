﻿using Caliburn.Micro;
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

        // convertimos la lista del modelo a un coleccion mas util para Caliburn
        private BindableCollection<Clasification> _clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());

        public BindableCollection<Clasification> Clasificacion
        {
            get { return _clasificaciones; }
            set
            {
                _clasificaciones = value;
                NotifyOfPropertyChange(() => Clasificacion);
            }
        }



        public void Agregar()
        {
            AdministrarModeloViewModel administrarModelos = new AdministrarModeloViewModel();
            IWindowManager manejador1 = new WindowManager();
            manejador1.ShowDialog(administrarModelos, null, null);
            Clasificaciones = null;
            
        }

        public void Editar()
        {
            //EditarMarcasViewModel editarMarcas = new EditarMarcasViewModel(ClasificacionSeleccionada);
            //IWindowManager manejador2 = new WindowManager();
            //manejador2.ShowDialog(editarMarcas, null, null);
            //Clasificaciones = null;
            //Clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
        }

      


    }
}
