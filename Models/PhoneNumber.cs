using System;
using TP2_AnimateursWPF_AP.Services;

namespace TP2_AnimateursWPF_AP.Models
{
    /// <summary>Wrap phone numbers in a class with formatter and validator.</summary>
    public class PhoneNumber
    {
        #region Static Services

        private static PhoneFormatter formatter;

        /// <summary>Force l'instantiation du formatteur de numéro.</summary>
        /// <remarks>
        ///     Normalement, un pattern de factory avec les informations
        ///     serait plus approprié, mais, dans un but de simplification,
        ///     on constructeur statique avec un formatteur static fait la job.
        /// </remarks>
        static PhoneNumber()
        {
            formatter = PhoneFormatter.Create();
        }

        #endregion

        #region Internal Data
        private string number;
        #endregion

        #region Data

        /// <summary>The actual phone number</summary>
        public string Number
        {
            get { return number; }
            private set
            {
                number = formatter.Validate(value)
                    ? value
                    : throw new ArgumentException("Invalid phone number");
            }
        }

        #endregion

        #region Constructors

        /// <summary>Basic constructor</summary>
        /// <param name="number">Actual phone number. Formatting doesn't matter.</param>
        /// <exception cref="ArgumentException">
        ///   If <paramref name="number"/> is not a valid phone number as specified by <see cref="PhoneFormatter"/>.
        /// </exception>
        public PhoneNumber(string number)
        {
            Number = number;
        }

        #endregion

        #region Conversions

        /// <summary>Convert the number to a formatted string.</summary>
        /// <returns>A formatted phone number</returns>
        public override string ToString()
        {
            return formatter.Format(Number);
        }

        #endregion
    }
}
