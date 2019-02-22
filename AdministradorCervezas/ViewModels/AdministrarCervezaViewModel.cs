using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdministradorCervezas.ViewModels
{
    class AdministrarCervezaViewModel : Screen, INotifyPropertyChanged
    {
        #region Atributos

        // Colecciones de Datos
        private BindableCollection<Beer> _cervezas;
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

        // Ruta del servidor donde se guardan las imagenes
        private string _fuenteImagenes = @"ftp://localhost/images/";
        // Guarda la ruta del explorador de archivos de la imagen
        private string _rutaImagen;
        // Guarda la extension de la imagen subida por el usuario
        private string _extensionImagen;

        #endregion

        #region Propiedades

        /// <summary>
        /// Lista de Cervezas
        /// </summary>
        public BindableCollection<Beer> Cervezas
        {
            get
            {
                return _cervezas;
            }

            set
            {
                _cervezas = value;
            }
        }

        /// <summary>
        /// Unidades de Medida
        /// </summary>
        public BindableCollection<string> UnidadesDeMedida
        {
            get { return _unidadesDeMedida; }
            set { _unidadesDeMedida = value; }
        }

        /// <summary>
        /// Tipos de Presentacion
        /// </summary>
        public BindableCollection<string> Tipos
        {
            get { return _tipos; }
            set { _tipos = value; }
        }

        /// <summary>
        /// Tipos de Fermentacion
        /// </summary>
        public BindableCollection<string> TiposFermentacion
        {
            get { return _tiposFermentacion; }
            set { _tiposFermentacion = value; }
        }

        /// <summary>
        /// Marcas
        /// </summary>
        public BindableCollection<Brand> Marcas
        {
            get { return _marcas; }
            set { _marcas = value; }
        }

        /// <summary>
        /// Clasificaciones
        /// </summary>
        public BindableCollection<Clasification> Clasificaciones
        {
            get { return _clasificaciones; }
            set { _clasificaciones = value; }
        }

        /// <summary>
        /// Paises
        /// </summary>
        public BindableCollection<Country> Paises
        {
            get { return _paises; }
            set
            {
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
                // Avisamos que cambio el contenido seleccionado
                NotifyOfPropertyChange(() => Contenido);

                // Avisamos al metodo que activa el boton de carga de imagen
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
                NotifyOfPropertyChange(() => PuedesVaciarCampos);
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

        #endregion

        #region Botones

        /// <summary>
        /// Permite cargar una imagen en la ventana
        /// </summary>
        public void CargarImagen()
        {
            // Escogemos la imagen
            OpenFileDialog cargaImg = new OpenFileDialog();

            // Filtramos los tipos de archivo
            cargaImg.Filter = "Imagenes (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            // Asignamos valores de imagen cargada
            if (cargaImg.ShowDialog() == true)
            {
                _rutaImagen = cargaImg.FileName;

                // Obtenemos extension
                _extensionImagen = _rutaImagen.Split('.')[1];

                // Cargamos en interfaz
                ImagenCerveza = new BitmapImage(new Uri(cargaImg.FileName, UriKind.Absolute));
            }
        }

        /// <summary>
        /// Guardamos la cerveza creada en la base de datos
        /// </summary>
        public void Guardar()
        {
            // Preguntamos
            MessageBoxResult resultado = MessageBox.Show("Estas seguro de guardar esta cerveza?", "Guardando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
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
                // Subimos imagen a servidor

                // Datos ftp
                string ftpuser = "ftpuser";
                string pass = "";
                string rutaFTPImagen = _fuenteImagenes + nueva.Image;

                // Cliente ftp
                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(ftpuser, pass);
                client.UploadFile(rutaFTPImagen, WebRequestMethods.Ftp.UploadFile, _rutaImagen);

                // Agregamos a base de datos
                nueva.Add();
                NotifyOfPropertyChange(() => Cervezas);

                // Limpiamos la forma
                Limpiar();
            }
        }

        /// <summary>
        /// Reiniciamos el Formulario
        /// </summary>
        public void Reiniciar()
        {
            MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de reiniciar la forma?", "Eliminando", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                // llamamos a limpiar
                Limpiar();
            }
        }

        // Lógica de activacion de controles

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

        /// <summary>
        /// Determina si puedes guardar la cerveza
        /// </summary>
        public bool PuedesCrearCerveza
        {
            get
            {
                return ImagenCerveza != null && Contenido != 0 && Precio != 0 && GradoAlcohol != 0;
            }
        }

        /// <summary>
        /// Determina si puedes reiniciar la forma
        /// </summary>
        public bool PuedesVaciarCampos
        {
            get
            {
                return MarcaSeleccionada != null;
            }
        }

        #endregion

        #region MetodosClase

        /// <summary>
        /// Este limpia sin preguntar
        /// </summary>
        public void Limpiar()
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

        /// <summary>
        /// Generar Nombre Imagen
        /// </summary>
        /// <returns></returns>
        private string generaNombreImagen()
        {
            // Obtenemos lista de cervezas para comparar nombres
            List<Beer> cervezas = Beer.GetAll();

            // Prefijo de Nombre de Imagen
            string prefijo = "beer";

            // Contador que cambia nombre de imagen prefijo + contador = nombre
            int contador = 0;

            // Guarda el nuevo nombre
            string nuevoNombre;

            // Guarda si existe una imagen de igual nombre
            bool existe = false;

            // Iteramos cervezas para obtener nombres
            foreach (Beer cerveza in cervezas)
            {
                nuevoNombre = prefijo + contador;
                existe = false;

                // Comparamos un nombre con todas las cervezas
                foreach (Beer cerveza2 in cervezas)
                {
                    // si existe seguimos iterando 
                    if (nuevoNombre == cerveza2.Image.Split('.')[0])
                    {
                        existe = true;
                    }
                }

                // si no lo usamos
                if (!existe)
                {
                    // si no existe devolvemos el nombre de la imagen
                    return nuevoNombre + "." + _extensionImagen;
                }
                // le sumamos uno al nombre de la imagen
                contador++;
            }

            // si no llegara a poder crearse un nombre regresamos un nulo
            return null;
        }

        #endregion
    }
}
