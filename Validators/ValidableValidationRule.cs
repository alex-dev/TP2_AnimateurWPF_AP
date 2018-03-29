﻿using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace TP2_AnimateursWPF_AP.Validators
{
    public class ValidableValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            return new ValidationResult(
                (((BindingGroup)value).Items[0] as IValidable)?.IsValid(culture) ?? false,
                null);
        }
    }
}
