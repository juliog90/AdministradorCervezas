﻿using Caliburn.Micro;
using System;

namespace AdministradorCervezas.ViewModels
{
    class EditarTipoViewModel : Screen
    {
        private BindableCollection<string> _colores = new BindableCollection<string>(Enum.GetNames(typeof(Color)));
        private BindableCollection<string> _categorias = new BindableCollection<string>(Enum.GetNames(typeof(CategoryType)));
        private string _nombreTipo;
        private string _tipoCodigo;
        private string _name;
        private string _colorSeleccionado;
        private string _categoriaSeleccionada;

        public BindableCollection<string> Colores
        {
            get { return _colores; }
            set
            {
                _colores = value;
                NotifyOfPropertyChange(() => Colores);
            }
        }

        public BindableCollection<string> Categorias
        {
            get { return _categorias; }
            set
            {
                _categorias = value;
                NotifyOfPropertyChange(() => Categorias);
            }
        }

        public string NombreTipo
        {
            get { return _nombreTipo; }
            set
            {
                _nombreTipo = value;
                NotifyOfPropertyChange(() => NombreTipo);
                NotifyOfPropertyChange(() => PuedesEscogerTipo);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => PuedeGuardar);
            }
        }

        public string ColorSeleccionado
        {
            get { return _colorSeleccionado; }
            set
            {
                _colorSeleccionado = value;
                NotifyOfPropertyChange(() => ColorSeleccionado);
                NotifyOfPropertyChange(() => PuedeGuardar);
            }
        }

        public string CategoriaSeleccionada
        {
            get { return _categoriaSeleccionada; }
            set
            {
                _categoriaSeleccionada = value;
                NotifyOfPropertyChange(() => CategoriaSeleccionada);
                NotifyOfPropertyChange(() => PuedeGuardar);
            }
        }

        public EditarTipoViewModel(BeerType editarTipo)
        {
            _nombreTipo = editarTipo.Name;
            _tipoCodigo = editarTipo.Id;

            for (int i = 0; i < Colores.Count; i++)
            {
                if (Enum.GetName(typeof(Color), editarTipo.Color) == Colores[i])
                {
                    ColorSeleccionado = Colores[i];
                    break;
                }
            }

            for (int i = 0; i < Categorias.Count; i++)
            {
                if (Enum.GetName(typeof(CategoryType), editarTipo.Category) == Categorias[i])
                {
                    CategoriaSeleccionada = Categorias[i];
                    break;
                }
            }
        }

        public bool PuedeGuardar
        {
            get
            {
                return ColorSeleccionado != null && CategoriaSeleccionada != null && !string.IsNullOrWhiteSpace(NombreTipo);
            }
        }

        public bool PuedesEscogerTipo
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreTipo);
            }
        }

        public void Guardar()
        {
            BeerType nuevoTipo = new BeerType();
            nuevoTipo.Id = _tipoCodigo;
            nuevoTipo.Name = NombreTipo;
            nuevoTipo.Color = (Color)Enum.Parse(typeof(Color), ColorSeleccionado);
            nuevoTipo.Category = (CategoryType)Enum.Parse(typeof(CategoryType), CategoriaSeleccionada);
            nuevoTipo.Edit();
        }

        public void Reiniciar()
        {
            NombreTipo = null;
            ColorSeleccionado = null;
            CategoriaSeleccionada = null;
        }
    }
}
