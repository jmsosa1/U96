using System.Globalization;
using System.Windows.Controls;


namespace UIDESK.validacion
{
    public class ReglaTextoNumero : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int numero;
            if (!int.TryParse((string)value, out numero))
            {
                return new ValidationResult(false, "Formato de numero incorrecto");
            }
            if (numero == 0)
            {
                return new ValidationResult(false, "El numero no puede ser cero");
            }
            return new ValidationResult(true, null);
        }
    }
}
