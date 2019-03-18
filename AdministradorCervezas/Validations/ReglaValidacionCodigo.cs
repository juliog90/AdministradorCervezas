using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdministradorCervezas.Validations
{
    class ReglaValidacionCodigo : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var texto = "";

            try
            {
                if (((string)value).Length > 0)
                    texto = (string)value;
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((texto.Length < Min) || (texto.Length > Max))
            {
                return new ValidationResult(false,
                    "Introduce tres caracteres..." + Min + " - " + Max + ".");
            }
            return new ValidationResult(true, null);
        }
    }
}
