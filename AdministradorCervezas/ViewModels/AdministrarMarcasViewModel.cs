using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.ViewModels
{
    class AdministrarMarcasViewModel : Screen
    {
        private string  _nombrePais;

        public string  NombrePais
        {
            get { return _nombrePais; }
            set { _nombrePais = value; }
        }

        private string _nombreMarca;

        public string NombreMarca
        {
            get { return _nombreMarca; }
            set { _nombreMarca = value; }
        }

        public bool PuedeGuardar
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombrePais) && !string.IsNullOrWhiteSpace(NombreMarca);
            }
        }

        public bool PuedeReiniciar
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombrePais) || !string.IsNullOrWhiteSpace(NombreMarca);
            }
        }

        public bool PuedesEscribirNombrePais
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreMarca);
            }
        }
    }
}
