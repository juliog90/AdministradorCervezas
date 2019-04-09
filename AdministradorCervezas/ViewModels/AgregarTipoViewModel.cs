using Caliburn.Micro;
using System;

namespace AdministradorCervezas.ViewModels
{
    public class AgregarTipoViewModel : Screen
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

        public BindableCollection <string> Categorias
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
                NotifyOfPropertyChange(() => PuedesEscogerColor);
            }
        }

        public string CodigoTipo
        {
            get { return _tipoCodigo; }
            set
            {
                _tipoCodigo = value;
                NotifyOfPropertyChange(() => CodigoTipo);
                NotifyOfPropertyChange(() => PuedesEscogerColor);
                NotifyOfPropertyChange(() => PuedeGuardar);
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
                NotifyOfPropertyChange(() => PuedesEscogerCategoria);
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

        public bool PuedeGuardar
        {
            get
            {
                return ColorSeleccionado != null && CategoriaSeleccionada != null;
            }
        }

        public bool PuedesEscogerColor
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreTipo) && !string.IsNullOrWhiteSpace(CodigoTipo);
            }
        }

        public bool PuedesEscogerCategoria
        {
            get
            {
                return ColorSeleccionado != null;
            }
        }

        public void Guardar()
        {
            BeerType nuevoTipo = new BeerType();
            nuevoTipo.Id = CodigoTipo;
            nuevoTipo.Name = NombreTipo;
            nuevoTipo.Color = (Color)Enum.Parse(typeof(Color) , ColorSeleccionado);
            nuevoTipo.Category = (CategoryType)Enum.Parse(typeof(CategoryType), CategoriaSeleccionada);
            nuevoTipo.Add();
            Reiniciar();
        }

        public void Reiniciar()
        {
            NombreTipo = null;
            CodigoTipo = null;
            ColorSeleccionado = null;
            CategoriaSeleccionada = null;
        }
    }
}
