using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdministradorCervezas.ViewModels
{
    class EditarCervezaViewModel : Screen, INotifyPropertyChanged
    {
        // Atributos de clase
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
        private int _id;
        private ImageSource _imagenCerveza;
        private string _imagenNombre;
        private Beer _editarCerveza;

        // Actualmente seleccionado
        private Brand _marcaSeleccionada;
        private string _tipoSeleccionado;
        private string _tiposFermentacionSeleccionado;
        private string _unidadDeMedidaSeleccionada;
        private Country _paisSeleccionado;
        private Clasification _clasificacionSeleccionada;

        //Nombre Marca, Pais, Tipo y Clasificacion
        private string _name;

        // ruta de imagen
        private string path;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        // Propiedades
        public BindableCollection<string> UnidadesDeMedida
        {
            get { return _unidadesDeMedida; }
        }

        public EditarCervezaViewModel(Beer editarCerveza)
        {
            _editarCerveza = editarCerveza;

            for (int i = 0; i < Marcas.Count; i++)
            {
                if (editarCerveza.Brand.Id == Marcas[i].Id)
                {
                    MarcaSeleccionada = Marcas[i];
                    break;
                }
            }

            for (int i = 0; i < Clasificaciones.Count; i++)
            {
                if (editarCerveza.Clasification.Code == Clasificaciones[i].Code)
                {
                    ClasificacionSeleccionada = Clasificaciones[i];
                    break;
                }
            }

            for (int i = 0; i < Paises.Count; i++)
            {
                if (editarCerveza.Brand.Country.Id == Paises[i].Id)
                {
                    PaisSeleccionado = Paises[i];
                    break;
                }
            }

            for (int i = 0; i < Tipos.Count; i++)
            {
                if (Enum.GetName(typeof(PresentationType), editarCerveza.Presentation) == Tipos[i])
                {
                    TipoSeleccionado = Tipos[i];
                    break;
                }
            }

            for (int i = 0; i < UnidadesDeMedida.Count; i++)
            {
                if (Enum.GetName(typeof(MeasurementUnit), editarCerveza.MeasurementUnit) == UnidadesDeMedida[i])
                {
                    UnidadDeMedidaSeleccionada = UnidadesDeMedida[i];
                    break;
                }
            }

            for (int i = 0; i < TiposFermentacion.Count; i++)
            {
                if (Enum.GetName(typeof(Fermentation), editarCerveza.Fermlevel) == TiposFermentacion[i])
                {
                    TiposFermentacionSeleccionado = TiposFermentacion[i];
                    break;
                }
            }

            Contenido = editarCerveza.Content;
            Precio = editarCerveza.Price;
            GradoAlcohol = editarCerveza.GradoAlcohol;
            _id = editarCerveza.Id;
            MemoryStream ms = new MemoryStream(editarCerveza.Image);
            Bitmap mapaDeBits = new Bitmap(ms);
            ImagenCerveza = BitmapToImageSource(mapaDeBits);
        }

        /// <summary>
        /// Tipos de Presentacion
        /// </summary>
        public BindableCollection<string> Tipos
        {
            get { return _tipos; }
        }

        /// <summary>
        /// Tipos de Fermentacion
        /// </summary>
        public BindableCollection<string> TiposFermentacion
        {
            get { return _tiposFermentacion; }
        }

        /// <summary>
        /// Marcas
        /// </summary>
        public BindableCollection<Brand> Marcas
        {
            get { return _marcas; }
        }

        /// <summary>
        /// Clasificaciones
        /// </summary>
        public BindableCollection<Clasification> Clasificaciones
        {
            get { return _clasificaciones; }
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

        // Botones

        // Cargamos imagen local
        public void CargarImagen()
        {
            // Escogemos la imagen
            OpenFileDialog cargaImg = new OpenFileDialog();

            if (cargaImg.ShowDialog() == true)
            {
                ImagenCerveza = new BitmapImage(new Uri(cargaImg.FileName, UriKind.Absolute));
            }
        }

        public bool ImagenCargada
        {
            get
            {
                return ImagenCerveza == null;
            }
        }

        /// <summary>
        /// Guardamos la cerveza creada en la base de datos
        /// </summary>
        public void Actualizar()
        {
            Beer nueva = new Beer();
            nueva.Id = _id;
            nueva.Brand = MarcaSeleccionada;
            nueva.Clasification = ClasificacionSeleccionada;
            nueva.Content = Contenido;
            nueva.Price = Precio;
            nueva.GradoAlcohol = GradoAlcohol;

            if(ImagenCargada)
            {
                nueva.Image = GenerarImagen();
            }
            else
            {
                nueva.Image = _editarCerveza.Image;
            }

            nueva.Image = _editarCerveza.Image;
            // Convertimos de String a Enum
            nueva.MeasurementUnit = (MeasurementUnit)Enum.Parse(typeof(MeasurementUnit), UnidadDeMedidaSeleccionada);
            nueva.Fermlevel = (Fermentation)Enum.Parse(typeof(Fermentation), TiposFermentacionSeleccionado);
            nueva.Presentation = (PresentationType)Enum.Parse(typeof(PresentationType), TipoSeleccionado);

            // Agregamos los cambios a la base de datos
            nueva.Edit();
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

        public Beer EditarCerveza
        {
            get
            {
                return _editarCerveza;
            }

            set
            {
                _editarCerveza = value;
            }
        }

        public string ImagenNombre
        {
            get
            {
                return ImagenNombre1;
            }

            set
            {
                ImagenNombre1 = value;
            }
        }

        public string ImagenNombre1
        {
            get
            {
                return _imagenNombre;
            }

            set
            {
                _imagenNombre = value;
            }
        }

        private byte[] GenerarImagen()
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader binary = new BinaryReader(file);
            byte[] datosImagen = binary.ReadBytes((int)file.Length);

            return datosImagen;
        }

        public BitmapImage BitmapToImageSource(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
