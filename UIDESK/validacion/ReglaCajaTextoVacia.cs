using System.Globalization;
using System.Windows.Controls;

namespace UIDESK.validacion
{
    public class ReglaCajaTextoVacia : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double n;
            if (double.TryParse(value.ToString(), out n))
            {
                return new ValidationResult(false, "No puede ser un numero ");
            }

            string contenido = value as string;
            if (contenido.Length == 0)
            {
                return new ValidationResult(false, string.Format("La cadena de entrada esta vacia"));
            }
            return ValidationResult.ValidResult;
        }
    }
}
