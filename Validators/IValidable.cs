using System.Globalization;

namespace TP2_AnimateursWPF_AP.Validators
{
    public interface IValidable
    {
        bool IsValid(CultureInfo culture);
    }
}
