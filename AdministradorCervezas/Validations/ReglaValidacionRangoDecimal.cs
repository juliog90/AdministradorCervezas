using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdministradorCervezas.Validations
{
    public class ReglaValidacionRangoDecimal : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double cantidad = 0;
            var texto = "";

            try
            {
                if (((string)value).Length > 0)
                {
                    cantidad = Double.Parse((string)value, CultureInfo.CurrentCulture); 
                    texto = Convert.ToString(cantidad); 
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "No es valido o " + e.Message);
            }

            if ((cantidad < Min) || (cantidad > Max))
            {
                return new ValidationResult(false,
                    "Introduce una cantidad en el rango..." + Min + " - " + Max + ".");
            }

            if (texto.Any(char.IsWhiteSpace))
            {
                return new ValidationResult(false,
                    "No introduzcas espacios");
            }
            return new ValidationResult(true, null);
        }
    }
}
