using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace UIDESK.validacion
{
    class ReglaCorreoElectronicoValido : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex expresion = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            Match match = expresion.Match(value.ToString());
            if (match == null || match == Match.Empty)
            {
                return new ValidationResult(false, "correo electronico invalido");
            }
            else
            {
                return ValidationResult.ValidResult;
            }




        }
        public string Expresion { get; set; }
    }
}
