﻿using AdministradorCervezas.Validations;
using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdministradorCervezas.ViewModels
{
    class AgregarCervezaViewModel : Screen, INotifyPropertyChanged
    {
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

        // ruta de imagen
        private string path;

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
        /// <summary>
        /// <summary>
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
                NotifyOfPropertyChange(() => PuedesCrearCerveza);
            }
        }

        /// <summary>
        /// Precio
        /// </summary>
        public double Precio
        {
            get { return _precio; }
            set
            {
                _precio = value;
                NotifyOfPropertyChange(() => Precio);
                NotifyOfPropertyChange(() => PuedesCrearCerveza);
            }
        }

        /// <summary>
        /// Grado de Alcohol
        /// </summary>
        public double GradoAlcohol
        {
            get { return _gradoAlcohol; }
            set
            {
                _gradoAlcohol = value;
                NotifyOfPropertyChange(() => GradoAlcohol);
                NotifyOfPropertyChange(() => PuedesCrearCerveza);
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
                NotifyOfPropertyChange(() => ImagenCargada);
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


        /// <summary>
        /// Permite cargar una imagen en la ventana
        /// </summary>
        public void CargarImagen()
        {
            // Escogemos la imagen
            OpenFileDialog cargaImg = new OpenFileDialog
            {

                // Filtramos los tipos de archivo
                Filter = "Imagenes (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            // Asignamos valores de imagen cargada
            if (cargaImg.ShowDialog() == true)
            {
                // Cargamos en interfaz
                ImagenCerveza = new BitmapImage(new Uri(cargaImg.FileName, UriKind.Absolute));
                path = cargaImg.FileName;
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
                nueva.Image = GenerarImagen();
                // Convertimos de String a Enum
                nueva.MeasurementUnit = (MeasurementUnit)Enum.Parse(typeof(MeasurementUnit), UnidadDeMedidaSeleccionada);
                nueva.Fermlevel = (Fermentation)Enum.Parse(typeof(Fermentation), TiposFermentacionSeleccionado);
                nueva.Presentation = (PresentationType)Enum.Parse(typeof(PresentationType), TipoSeleccionado);
                // Subimos imagen a servidor


                // Agregamos a base de datos
                nueva.Add();
                NotifyOfPropertyChange(() => Cervezas);

                // Limpiamos la forma
                Limpiar();
            }
            MessageBox.Show("Cerveza guardada","Completo",MessageBoxButton.OK, MessageBoxImage.Information);
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
        /// Determina si puedes guardar la cerveza
        /// </summary>
        public bool PuedesCrearCerveza
        {
            get
            {
                return PuedesSeleccionarGradoAlcohol && ImagenCerveza != null;
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

        public bool ImagenCargada
        {
            get
            {
                return ImagenCerveza == null;
            }
        }

        /// <summary>
        /// Limpia los campos de la forma 
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

        private byte[] GenerarImagen()
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader binary = new BinaryReader(file);
            byte[] datosImagen = binary.ReadBytes((int)file.Length);

            return datosImagen;
        }

    }
}
