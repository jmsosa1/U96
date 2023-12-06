using System.Globalization;
using System.Windows.Controls;

namespace UIDESK.validacion
{
    class ReglaComboBoxVacio : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // para validar si un combobox tiene algun elemento seleccionado debemos evaluar
            //el valor de la propiedad selectedindex . si el valor es -1 indica que no hay elemento seleccionado


            if (value is ComboBoxItem)
            {
                return new ValidationResult(false, "Debe seleccionar un item ");
            }
            return new ValidationResult(true, null);
        }
    }
}
