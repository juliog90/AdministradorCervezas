using Caliburn.Micro;
using System;

namespace AdministradorCervezas.ViewModels
{
    class AdministrarTipoViewModel : Screen
    {
        private BindableCollection<string> _colores = new BindableCollection<string>(Enum.GetNames(typeof(Color)));
        private BindableCollection<string> _categorias = new BindableCollection<string>(Enum.GetNames(typeof(CategoryType)));

        public BindableCollection<string> Colores
        {
            get { return _colores; }
            set
            {
                _colores = value;
                NotifyOfPropertyChange(() => Colores);
            }
        }

        public BindableCollection <string> Categorias
        {
            get { return _categorias; }
            set
            {
                _categorias = value;
                NotifyOfPropertyChange(() => Categorias);
            }
        }

        private string _nombreTipo;

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

        private string _tipoCodigo;

        public string TipoCodigo
        {
            get { return _tipoCodigo; }
            set
            {
                _tipoCodigo = value;
                NotifyOfPropertyChange(() => TipoCodigo);
            }
        }

        private string _name;

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


        private string _colorSeleccionado;

        public string ColorSeleccionado
        {
            get { return _colorSeleccionado; }
            set
            {
                _colorSeleccionado = value;
                NotifyOfPropertyChange(() => _colorSeleccionado);
                NotifyOfPropertyChange(() => PuedeGuardar);
            }
        }

        private string _categoriaSeleccionada;

        public string CategoriaSeleccionada
        {
            get { return _categoriaSeleccionada; }
            set
            {
                _categoriaSeleccionada = value;
                NotifyOfPropertyChange(() => _categoriaSeleccionada);
                NotifyOfPropertyChange(() => PuedeGuardar);
            }
        }

        public bool PuedeGuardar
        {
            get
            {
                return ColorSeleccionado != null && CategoriaSeleccionada != null && !string.IsNullOrWhiteSpace(NombreTipo) && !string.IsNullOrWhiteSpace(TipoCodigo);
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
            nuevoTipo.Id = TipoCodigo;
            nuevoTipo.Name = NombreTipo;
            nuevoTipo.Color = (Color) Enum.Parse(typeof(Color) , ColorSeleccionado);
            nuevoTipo.Category = (CategoryType)Enum.Parse(typeof(CategoryType), CategoriaSeleccionada);
            nuevoTipo.Add();
            Reiniciar();
        }

        public void Reiniciar()
        {
            NombreTipo = "";
            TipoCodigo = "";
            ColorSeleccionado= null;
            CategoriaSeleccionada = null;
        }
    }
}
