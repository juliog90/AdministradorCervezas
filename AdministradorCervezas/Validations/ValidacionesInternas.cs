using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdministradorCervezas.Validations
{
    public class ValidacionesInternas
    {
        public int min;
        public int max;
        private Regex letrasNumeros = new Regex(@"^[aA-zZ0-9]+");

        public bool ValidaloEntero(int numero)
        {
            var cantidad = 0;
            string texto = Convert.ToString(numero);

            try
            {
                if (texto.Length > 0)
                {
                    cantidad = int.Parse(texto);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            if ((cantidad < min) || (cantidad > max))
            {
                return false;
            }
            if (texto.Any(char.IsWhiteSpace))
            {
                return false;
            }

            return true;
        }

        public bool ValidaloDouble(double numero)
        {
            double cantidad = 0;
            string texto = Convert.ToString(numero);

            try
            {
                if (texto.Length > 0)
                {
                    cantidad = Double.Parse(texto, CultureInfo.CurrentCulture);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            if ((cantidad < min) || (cantidad > max))
            {
                return false;
            }

            if (texto.Any(char.IsWhiteSpace))
            {
                return false;
            }

            return true;
        }

        public bool ValidaloCodigo(string texto)
        {
            if ((texto.Length < min) || (texto.Length > max))
            {
                return false;
            }
            if (texto.Any(char.IsLower))
            {
                return false;
            }
            if (texto.Any(char.IsWhiteSpace))
            {
                return false;
            }

            if (!letrasNumeros.IsMatch(texto))
            {
                return false;
            }

            return true;
        }

        public bool ValidaloNombre(string texto)
        {
            if ((texto.Length < min) || (texto.Length > max))
            {
                return false;
            }

            if (texto[0] == ' ')
            {
                return false;
            }

            if (!letrasNumeros.IsMatch(texto))
            {
                return false;
            }

            return true;
        }
    }
}
