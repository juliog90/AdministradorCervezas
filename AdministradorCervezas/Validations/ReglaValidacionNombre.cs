using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdministradorCervezas.Validations
{
    public class ReglaValidacionNombre : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var texto = "";
            Regex letrasNumeros = new Regex(@"^[aA-zZ0-9]+");

            try
            {
                if (((string)value).Length > 0)
                    texto = (string)value;
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "No es valido o" + e.Message);
            }
            if(texto == "")
            {
                return new ValidationResult(false,
                    "Introduce un valor");
            }

            if ((texto.Length < Min) || (texto.Length > Max))
            {
                return new ValidationResult(false,
                    "Introduce menos de 20 caracteres..." + Min + " - " + Max + ".");
            }

            if (texto[0] == ' ')
            {
                return new ValidationResult(false,
                    "No introduzcas espacios");
            }

            if(!letrasNumeros.IsMatch(texto))
            {
                return new ValidationResult(false, "No introduzcas simbolos");
            }

            return new ValidationResult(true, null);
        }
    }
}
