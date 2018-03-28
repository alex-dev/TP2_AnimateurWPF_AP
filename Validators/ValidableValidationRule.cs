using System.Globalization;
using System.Windows.Controls;

namespace TP2_AnimateursWPF_AP.Validators
{
    public class ValidableValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            return new ValidationResult(
                (value as IValidable)?.IsValid(culture) ?? false,
                null);
        }
    }
}
