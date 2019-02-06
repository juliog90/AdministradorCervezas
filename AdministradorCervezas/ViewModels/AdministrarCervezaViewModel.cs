using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdministradorCervezas.ViewModels
{
    class AdministrarCervezaViewModel : Screen, INotifyPropertyChanged
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
        private ImageSource _imagenCerveza;

        // Actualmente seleccionado
        private Brand _marcaSeleccionada;
        private string _tipoSeleccionado;
        private string _tiposFermentacionSeleccionado;
        private string _unidadDeMedidaSeleccionada;
        private Country _paisSeleccionado;
        private Clasification _clasificacionSeleccionada;

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

        /// <summary>
        /// Contenido
        /// </summary>
        public double Contenido
        {
            get { return _contenido; }
            set
            {
                _contenido = value;
                NotifyOfPropertyChange(() => Contenido);
                NotifyOfPropertyChange(() => PuedesCargarImagen);
            }
        }

        /// <summary>
        /// Precio
        /// </summary>
        public double Precio
        {
            get { return _gradoAlcohol; }
            set
            {
                _gradoAlcohol = value;
                NotifyOfPropertyChange(() => Precio);
                NotifyOfPropertyChange(() => PuedesSeleccionarContenido);
            }
        }

        /// <summary>
        /// Grado de Alcohol
        /// </summary>
        public double GradoAlcohol
        {
            get { return _precio; }
            set
            {
                _precio = value;
                NotifyOfPropertyChange(() => GradoAlcohol);
                NotifyOfPropertyChange(() => PuedesSeleccionarPrecio);
            }
        }

        /// <summary>
        /// Imagen Seleccionada
        /// </summary>
        public ImageSource ImagenCerveza
        {
            get { return _imagenCerveza; }
            set
            {
                _imagenCerveza = value;
                NotifyOfPropertyChange(() => ImagenCerveza);
                NotifyOfPropertyChange(() => PuedesCrearCerveza);
            }
        }
        
        /// <summary>
        /// Tipo de Fermentacion Seleccionado
        /// </summary>
        public string TiposFermentacionSeleccionado
        {
            get { return _tiposFermentacionSeleccionado; }
            set
            {
                _tiposFermentacionSeleccionado = value;
                NotifyOfPropertyChange(() => TiposFermentacionSeleccionado);
                NotifyOfPropertyChange(() => PuedesSeleccionarClasificacion);
            }
        }

        /// <summary>
        /// Tipo de Presentacion Seleccionado
        /// </summary>
        public string TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set
            {
                _tipoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoSeleccionado);
                NotifyOfPropertyChange(() => PuedesSeleccionarFermentado);
            }
        }

        /// <summary>
        /// Clasificacion Seleccionada
        /// </summary>
        public Clasification ClasificacionSeleccionada
        {
            get
            {
                return _clasificacionSeleccionada;
            }

            set
            {
                _clasificacionSeleccionada = value;
                NotifyOfPropertyChange(() => ClasificacionSeleccionada);
                NotifyOfPropertyChange(() => PuedesSeleccionarUnidadesDeMedida);
            }
        }

        /// <summary>
        /// Unidad de Medida Seleccionada
        /// </summary>
        public string UnidadDeMedidaSeleccionada
        {
            get { return _unidadDeMedidaSeleccionada; }
            set
            {
                _unidadDeMedidaSeleccionada = value;
                NotifyOfPropertyChange(() => UnidadDeMedidaSeleccionada);
                NotifyOfPropertyChange(() => PuedesSeleccionarGradoAlcohol);
            }
        }

        /// <summary>
        /// Marca Seleccionada
        /// </summary>
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
                NotifyOfPropertyChange(() => PuedesSeleccionarPaises);
            }
        }

        /// <summary>
        /// Pais seleccionado
        /// </summary>
        public Country PaisSeleccionado
        {
            get { return _paisSeleccionado; }
            set
            {
                _paisSeleccionado = value;
                NotifyOfPropertyChange(() => PaisSeleccionado);
                NotifyOfPropertyChange(() => PuedesSeleccionarTipos);
            }
        }

        /// <summary>
        /// Guarda el nombre de los enumeradores
        /// </summary>
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

        // Cargamos imagen local
        public void CargarImagen()
        {
            // Escogemos la imagen
            OpenFileDialog cargaImg = new OpenFileDialog();
            if (cargaImg.ShowDialog() == true)
            {
                // la pasamos a la interfaz
                ImagenCerveza = new BitmapImage(new Uri(cargaImg.FileName, UriKind.Absolute));
            }
        }

        /// <summary>
        /// Guardamos la cerveza creada en la base de datos
        /// </summary>
        public void Guardar()
        { 
            Beer nueva = new Beer();
            nueva.Brand = MarcaSeleccionada;
            nueva.Clasification = ClasificacionSeleccionada;
            nueva.Content = Contenido;
            nueva.Price = Precio;
            nueva.GradoAlcohol = GradoAlcohol;
            nueva.Image = generaNombreImagen();
            // Convertimos de String a Enum
            nueva.MeasurementUnit = (MeasurementUnit)Enum.Parse(typeof(MeasurementUnit), UnidadDeMedidaSeleccionada);
            nueva.Fermlevel = (Fermentation)Enum.Parse(typeof(Fermentation), TiposFermentacionSeleccionado);
            nueva.Presentation = (PresentationType)Enum.Parse(typeof(PresentationType), TipoSeleccionado);
            // Agregamos
            nueva.Add();
        }

        /// <summary>
        /// Reiniciamos el Formulario
        /// </summary>
        public void Reiniciar()
        {
            MarcaSeleccionada = null;
            ClasificacionSeleccionada = null;
            PaisSeleccionado = null;
            Contenido = 0;
            Precio = 0;
            GradoAlcohol = 0;
            ImagenCerveza = null;
            UnidadDeMedidaSeleccionada = null;
            TiposFermentacionSeleccionado = null;
            TipoSeleccionado = null;
        }

        // logica de activacion de controles

        /// <summary>
        /// Determina si puedes seleccionar paises
        /// </summary>
        public bool PuedesSeleccionarPaises
        {
            get
            {
                return MarcaSeleccionada != null;
            }
        }

        /// <summary>
        /// Determina si puedes seleccionar tipos de cervezas
        /// </summary>
        public bool PuedesSeleccionarTipos
        {
            get
            {
                return PaisSeleccionado != null;
            }
        }

        /// <summary>
        /// Determina si puedes seleccionar el tipo de fermentado
        /// </summary>
        public bool PuedesSeleccionarFermentado
        {
            get
            {
                return TipoSeleccionado != null;
            }
        }

        /// <summary>
        /// Determina si puedes seleccionar una clasificacion
        /// </summary>
        public bool PuedesSeleccionarClasificacion
        {
            get
            {
                return TiposFermentacionSeleccionado != null;
            }
        }

        /// <summary>
        /// Determina si puedes seleccionar medidas 
        /// </summary>
        public bool PuedesSeleccionarUnidadesDeMedida
        {
            get
            {
                return ClasificacionSeleccionada != null;
            }
        }

        /// <summary>
        /// Determina si puedes escoger un grado de alcohol
        /// </summary>
        public bool PuedesSeleccionarGradoAlcohol
        {
            get
            {
                return UnidadDeMedidaSeleccionada != null;
            }
        }

        /// <summary>
        /// Determina si puedes escoger un precio
        /// </summary>
        public bool PuedesSeleccionarPrecio
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Convert.ToString(GradoAlcohol)) && GradoAlcohol != 0;
            }
        }


        /// <summary>
        /// Determina si puedes escoger un contenido
        /// </summary>
        public bool PuedesSeleccionarContenido
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Convert.ToString(Precio)) && Precio != 0;
            }
        }

        /// <summary>
        /// Determina si puedes cargar una imagen
        /// </summary>
        public bool PuedesCargarImagen
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Convert.ToString(Contenido)) && Contenido != 0 && Precio != 0 && GradoAlcohol != 0;
            }
        }

        public bool PuedesCrearCerveza
        {
            get
            {
                return ImagenCerveza != null && Contenido != 0 && Precio != 0 && GradoAlcohol != 0;
            }
        }


        // metodos para la clase
        private string generaNombreImagen()
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

                contader++;
            }

            return null;
        }
    }
}
