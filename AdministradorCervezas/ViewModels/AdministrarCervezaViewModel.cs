using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AdministradorCervezas.ViewModels
{
    class AdministrarCervezaViewModel : Screen
    {
        // Colecciones de Datos
        private BindableCollection<Brand> _marcas = new BindableCollection<Brand>(Brand.GetAll());
        private BindableCollection<Country> _paises = new BindableCollection<Country>(Country.GetAll());
        private BindableCollection<Clasification> _clasificaciones = new BindableCollection<Clasification>(Clasification.GetAll());
        private BindableCollection<string> _tipos = new BindableCollection<string>(Enum.GetNames(typeof(PresentationType)));
        private BindableCollection<string> _tiposFermentacion = new BindableCollection<string>(Enum.GetNames(typeof(Fermentation)));
        private BindableCollection<string> _unidadesDeMedida = new BindableCollection<string>(Enum.GetNames(typeof(MeasurementUnit)));
        
        // Datos Unicos
        private double _contenido;
        private double _precio;
        private double _gradoAlcohol;
        private BitmapImage _imagenCerveza = new BitmapImage();

        // Actualmente seleccionado
        private Brand _marcaSeleccionada;
        private string _tipoSeleccionado;
        private string _tiposFermentacionSeleccionado;
        private string _unidadDeMedidaSeleccionada;
        private Country _paisSeleccionado;

        //Nombre Marca, Pais, Tipo y Clasificacion
        private string _name;

        //imagenes servidor
        private string _fuenteImagenes = @"http://localhost/the_brewery/images/";

        // Propiedades
        public BindableCollection<string> UnidadesDeMedida
        {
            get { return _unidadesDeMedida; }
            set { _unidadesDeMedida = value; }
        }

        public BindableCollection<string> Tipos
        {
            get { return _tipos; }
            set { _tipos = value; }
        }

        public BindableCollection<string> TiposFermentacion
        {
            get { return _tiposFermentacion; }
            set { _tiposFermentacion = value; }
        }

        public BindableCollection<Brand> Marcas
        {
            get { return _marcas; }
            set { _marcas = value; }
        }

        public BindableCollection<Clasification> Clasificaciones
        {
            get { return _clasificaciones; }
            set { _clasificaciones = value; }
        }

        public BindableCollection<Country> Paises
        {
            get { return _paises; }
            set {
                    _paises = value;
                }
        }

        public double Contenido
        {
            get { return _contenido; }
            set { _contenido = value; }
        }

        public double Precio
        {
            get { return _gradoAlcohol; }
            set { _gradoAlcohol = value; }
        }

        public double GradoAlcohol
        {
            get { return _precio; }
            set { _precio = value; }
        }

        public BitmapImage ImagenCerveza
        {
            get { return _imagenCerveza; }
            set
            {
                _imagenCerveza = value;
                NotifyOfPropertyChange(() => ImagenCerveza);
            }
        }
        
        public string TiposFermentacionSeleccionado
        {
            get { return _tiposFermentacionSeleccionado; }
            set { _tiposFermentacionSeleccionado = value; }
        }

        public string TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set { _tipoSeleccionado = value; }
        }

        public string UnidadDeMedidaSeleccionada
        {
            get { return _unidadDeMedidaSeleccionada; }
            set { _unidadDeMedidaSeleccionada = value; }
        }

        public Brand MarcaSeleccionada
        {
            get
            {
                return _marcaSeleccionada;
            }
            set
            {
                _marcaSeleccionada = value;
                NotifyOfPropertyChange(() => MarcaSeleccionada);
            }
        }

        public Country PaisSeleccionado
        {
            get { return _paisSeleccionado; }
            set
            {
                _paisSeleccionado = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        // Botones
        public void CargarImagen()
        {
            // Escogemos la imagen
            OpenFileDialog cargaImg = new OpenFileDialog();
            string rutaImagenLocal;
            if (cargaImg.ShowDialog() == true)
            {
                rutaImagenLocal = cargaImg.FileName;
                ImagenCerveza.CacheOption = BitmapCacheOption.OnLoad;
                ImagenCerveza.UriSource = new Uri(rutaImagenLocal);
            }
        }

        public string generaNombreImagen()
        {
            List<Beer> cervezas = Beer.GetAll();
            string prefijo = "beer";
            // no sirve contador???
            int contader = 0;
            string nuevoNombre; 
            int existente = 0;

            foreach(Beer cerveza in cervezas)
            {
                nuevoNombre = prefijo + contader;
                existente = 0;
                
                foreach(Beer cerveza2 in cervezas)
                {
                    if (nuevoNombre == cerveza2.Image)
                    {
                        existente++;
                    }
                } 

                if(existente == 0)
                {
                    return nuevoNombre;
                }
            }

            return null;
        }
    }
}
