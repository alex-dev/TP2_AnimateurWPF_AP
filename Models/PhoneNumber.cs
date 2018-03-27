using System;
using System.Runtime.Serialization;
using TP2_AnimateursWPF_AP.Services;

namespace TP2_AnimateursWPF_AP.Models
{
    public class PhoneNumber
    {
        private static PhoneFormatter formatter;

        /// <summary>Force l'instantiation du formatteur de numéro.</summary>
        /// <remarks>
        ///     Normalement, un pattern de factory avec les informations
        ///     serait plus approprié, mais, dans un but de simplification,
        ///     on constructeur statique avec un formatteur static fait la job.
        /// </remarks>
        static PhoneNumber()
        {
            formatter = new PhoneFormatter();
        }

        private string number;

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

        public PhoneNumber(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return formatter.Format(Number);
        }
    }
}
