using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    class EditarMarcasViewModel : Screen
    {
        private BindableCollection<Country>  _paises = new BindableCollection<Country>(Country.GetAll());

        public BindableCollection<Country>  Paises
        {
            get { return _paises; }
            set { _paises = value; }
        }

        public EditarMarcasViewModel(Brand editarMarca)
        {
            NombreMarca = editarMarca.Name;
            MarcaCodigo = editarMarca.Id;
            PaisSeleccionado = editarMarca.Country;
        }

        private string _nombreMarca;

        public string NombreMarca
        {
            get { return _nombreMarca; }
            set
            {
                _nombreMarca = value;
                NotifyOfPropertyChange(() => NombreMarca);
                NotifyOfPropertyChange(() => PuedesEscogerPais);
            }
        }

        private string _marcaCodigo;

        public string MarcaCodigo
        {
            get { return _marcaCodigo; }
            set
            {
                _marcaCodigo = value;
                NotifyOfPropertyChange(() => MarcaCodigo);
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
                NotifyOfPropertyChange(() => PuedeEditar);
            }
        }


        private Country _paisSeleccionado;

        public Country PaisSeleccionado
        {
            get { return _paisSeleccionado; }
            set
            {
                _paisSeleccionado = value;
                NotifyOfPropertyChange(() => PaisSeleccionado);
                NotifyOfPropertyChange(() => PuedeEditar);
            }
        }

        public bool PuedeEditar
        {
            get
            {
                return PaisSeleccionado != null && !string.IsNullOrWhiteSpace(NombreMarca) && !string.IsNullOrWhiteSpace(MarcaCodigo);
            }
        }

        public bool PuedesEscogerPais
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreMarca);
            }
        }

        public void Editar()
        {
            Brand nuevaMarca = new Brand();
            nuevaMarca.Id = _marcaCodigo;
            nuevaMarca.Name = NombreMarca;
            nuevaMarca.Country = PaisSeleccionado;
            nuevaMarca.Edit();
        }

        public void Reiniciar()
        {
            
        }
    }
}
