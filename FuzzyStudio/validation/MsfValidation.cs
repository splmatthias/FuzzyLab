using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace fuzzyStudio.validation
{
    public class MsfValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Length == 0)
                return new ValidationResult(false, "value cannot be empty.");

            var values = value.ToString().Split(';');
            double temp;
            if(values.Any(val => !double.TryParse(val, out temp)))
                return new ValidationResult(false, "invalid numeric value");
            
            return ValidationResult.ValidResult;
        }
    }
}
