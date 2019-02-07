﻿using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    public class AdministrarModeloViewModel : Screen
    {
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

        private BindableCollection<BeerType> _tipos;

        public BindableCollection<BeerType> Tipos
        {
            get { return _tipos; }
            set { _tipos = value; }
        }

        private BeerType _tipo;

        public BeerType Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public bool PuedesEscribirNombre
        {
            get 
            {
                return Tipo != null;
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

        }

    }
}