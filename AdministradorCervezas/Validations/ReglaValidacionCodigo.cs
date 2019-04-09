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
    public class ReglaValidacionCodigo : ValidationRule
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
                return new ValidationResult(false, "No es valido o " + e.Message);
            }

            if ((texto.Length < Min) || (texto.Length > Max))
            {
                return new ValidationResult(false,
                    "Introduce tres caracteres..." + Min + " - " + Max + ".");
            }
            if (texto.Any(char.IsLower))
            {
                return new ValidationResult(false,
                    "Introduce solo mayusculas..." + Min + " - " + Max + ".");
            }
            if(texto == null)
            {
                return new ValidationResult(false,
                    "Introduce un valor");
            }
            if (texto.Any(char.IsWhiteSpace))
            {
                return new ValidationResult(false,
                    "No introduzcas espacios");
            }

            if (!letrasNumeros.IsMatch(texto))
            {
                return new ValidationResult(false, "No introduzcas simbolos");
            }

            return new ValidationResult(true, null);
        }
    }
}
