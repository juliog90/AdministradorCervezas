using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdministradorCervezas.Validations
{
    public class ReglaValidacionRango : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var cantidad = 0;

            try
            {
                if (((string)value).Length > 0)
                    cantidad = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((cantidad < Min) || (cantidad > Max))
            {
                return new ValidationResult(false,
                    "Introduce una cantidad en el rango..." + Min + " - " + Max + ".");
            }
            return new ValidationResult(true, null);
        }
    }
    
}
