﻿using Caliburn.Micro;
using System.Windows;

namespace AdministradorCervezas.ViewModels
{
    public class AgregarClasificacionViewModel : Screen
    {
        private string _nombre;
        private string _codigo;
        private BindableCollection<BeerType> _tipos = new BindableCollection<BeerType>(BeerType.GetAll());
        private BeerType _tipoSeleccionado;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                NotifyOfPropertyChange(() => Nombre);
            }
        }

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                _codigo = value;
                NotifyOfPropertyChange(() => Codigo);
            }
        }

        public BindableCollection<BeerType> Tipos
        {
            get { return _tipos; }
        }

        public BeerType TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set
            {
                _tipoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoSeleccionado);
                NotifyOfPropertyChange(() => PuedesEscribirNombre);
            }
        }

        public bool PuedesEscribirNombre
        {
            get
            {
                return TipoSeleccionado != null;
            }
        }

        public bool PuedesEscibirCodigo
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Nombre);
            }
        }

        public void Guardar()
        {
            MessageBoxResult resultado = MessageBox.Show("Estas seguro de guardar esta clasificacion?", "Guardando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                Clasification clasificacion = new Clasification();
                clasificacion.Name = Nombre;
                clasificacion.Code = Codigo;
                clasificacion.BeerType = TipoSeleccionado;
                clasificacion.Add();
                Reiniciar();
            }
        }

        public void Reiniciar()
        {
            Nombre = "";
            Codigo = "";
            TipoSeleccionado = null;
        }
    }
}
