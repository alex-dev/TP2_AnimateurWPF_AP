using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using TP2_AnimateursWPF_AP.Models;
using TP2_AnimateursWPF_AP.Validators;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    /// <summary>
    ///   Interface la vue à l'<see cref="Animateur"/> tout en implémentant <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class AnimatorViewModel : INotifyPropertyChanged, IValidable
    {
        #region Internal Data
        private Animateur Animator { get; set; }
        private string _FirstName { get; set; }
        private string _LastName { get; set; }
        private PhoneNumber _Phone { get; set; }
        private List<Personnage> _Characters { get; set; }
        #endregion

        #region Data

        public string FirstName
        {
            get { return Animator?.Prenom ?? _FirstName; }
            set
            {
                if (value is null || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Un prénom ne peut pas être vide.");
                }
                else if (Animator is null)
                {
                    _FirstName = value;
                }
                else
                {
                    Animator.Prenom = value;
                }

                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return Animator?.Nom ?? _LastName; }
            set
            {
                if (value is null || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Un nom ne peut pas être vide.");
                }
                else if (Animator is null)
                {
                    _LastName = value;
                }
                else
                {
                    Animator.Nom = value;
                }

                OnPropertyChanged();
            }
        }
        public string Phone
        {
            get { return Animator?.Telephone?.ToString() ?? _Phone?.ToString(); }
            set
            {
                if (Animator is null)
                {
                    _Phone = new PhoneNumber(value);
                }
                else
                {
                    Animator.Telephone = new PhoneNumber(value);
                }

                OnPropertyChanged();
            }
        }
        public IReadOnlyCollection<Personnage> Characters
        {
            get { return Animator?.LstPersonnages ?? _Characters ?? (_Characters = new List<Personnage>()); }
        }

        #endregion

        #region Constructors

        /// <summary>Constructeur par défaut</summary>
        public AnimatorViewModel() : this(null) { }

        /// <summary>Constructeur de base</summary>
        /// <param name="animator">Animateur lié au vue modèle</param>
        public AnimatorViewModel(Animateur animator)
        {
            Animator = animator;
        }

        #endregion

        #region Methods

        /// <summary>Crée un <see cref="CharactersViewModel"/> correspondant aux personnage.</summary>
        /// <returns>
        ///   Item1: Le <see cref="CharactersViewModel"/>.
        ///   Item2: Un callback à exécuter pour updater avec le nouveau <see cref="CharactersViewModel"/>
        /// </returns>
        public Tuple<CharactersViewModel, Action<CharactersViewModel>> CreateCharactersViewModel()
        {
            return Tuple.Create<CharactersViewModel, Action<CharactersViewModel>>(
                new CharactersViewModel(Characters),
                CharactersUpdate);
        }

        /// <summary>Extrait le <see cref="Animateur"/>.</summary>
        /// <returns>Le <see cref="Animateur"/> wrapper par le <see cref="AnimatorViewModel"/></returns>
        /// <exception cref="InvalidOperationException">Lancer quand le <see cref="Animateur"/> est invalide.</exception>
        public Animateur Extract()
        {
            return IsValid(null)
                ? Animator
                : throw new InvalidOperationException("Animator is in an invalid state.");
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Animator is null && IsValid(null))
            {
                Animator = new Animateur(_FirstName, _LastName, _Phone) { LstPersonnages = _Characters };
            }

            if (!(PropertyChanged is null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void CharactersUpdate(CharactersViewModel characters)
        {
            var data = from character in characters.Characters
                       where character.IsValid(null)
                       select character.Extract();

            if (Animator is null)
            {
                _Characters = data.ToList();
            }
            else
            {
                Animator.LstPersonnages = data.ToList();
            }

            OnPropertyChanged("Characters");
        }

        #endregion

        #region IValidable

        public bool IsValid(CultureInfo culture)
        {
            return !(string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || Phone is null);
        }

        #endregion
    }
}
