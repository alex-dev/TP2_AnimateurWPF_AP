using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace TP2_AnimateursWPF_AP.Services
{
    /// <summary>Phopne numbers formatter and validator.</summary>
    /// <remarks>
    ///     Normalement, les informations des formats seraient conservées dans des fichiers ressources.
    ///     Pour simplifier, ces informations sont créées à la construction.
    /// </remarks>
    class PhoneFormatter
    {
        #region Singleton Pattern

        private static PhoneFormatter instance;

        /// <summary>Create a <see cref="PhoneFormatter"/> isntance if one doesn't already exist.</summary>
        /// <returns>A singleton <see cref="PhoneFormatter"/></returns>
        public static PhoneFormatter Create()
        {
            return instance ?? (instance = new PhoneFormatter());
        }

        #endregion

        #region Internal State

        private ReadOnlyDictionary<string, Tuple<Regex, string>> Formats { get; set; }

        #endregion

        #region Constructors

        private PhoneFormatter()
        {
            Formats = new ReadOnlyDictionary<string, Tuple<Regex, string>>(
                new Dictionary<string, Tuple<Regex, string>>
                {
                    { "CA_QC", Tuple.Create(
                        new Regex(@"1?((?:418)|(?:438)|(?:450)|(?:514)|(?:579)|(?:581)|(?:819)|(?:873))([2-9]\d{2})(\d{4})"),
                        "+1 $1 $2-$3") },
                    { "NANP", Tuple.Create(
                        new Regex(@"1?((?!(?:418)|(?:438)|(?:450)|(?:514)|(?:579)|(?:581)|(?:819)|(?:873))\d{3})([2-9]\d{2})(\d{4})"),
                        "+1-$1-$2-$3") }
                });
        }

        #endregion

        #region Methods

        /// <summary>Format a phone number depending on its origin.</summary>
        /// <param name="phone">Number only <see cref="string"/> validated by <see cref="PhoneFormatter"/></param>
        /// <returns><paramref name="phone"/> formatted</returns>
        public string Format(string phone)
        {
            var formatValue = (from format in Formats
                               where format.Value.Item1.Match(phone).Success
                               select format.Value).FirstOrDefault();

            if (!(formatValue is null))
            {
                phone = formatValue.Item1.Replace(phone, formatValue.Item2);
            }

            return phone;
        }

        /// <summary>Validate a phone number depending on its origin.</summary>
        /// <param name="phone">Phone number to validate</param>
        /// <returns>Is <paramref name="phone"/> valid</returns>
        public bool Validate(string phone)
        {
            phone = new string(phone.Where(Char.IsDigit).ToArray());

            var validFormats = from format in Formats
                               where format.Value.Item1.Match(phone).Success
                               select format.Value;

            return validFormats.Count() > 0;
        }

        #endregion
    }
}
