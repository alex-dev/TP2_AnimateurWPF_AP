using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
            get { return Animator?.LstPersonnages ?? new List<Personnage>(); }
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

        public CharactersViewModel CreateCharactersViewModel()
        {
            return new CharactersViewModel(Animator.LstPersonnages);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Animator is null && IsValid(null))
            {
                Animator = new Animateur(_FirstName, _LastName, _Phone);
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IValidable

        public bool IsValid(CultureInfo culture)
        {
            return !(FirstName is null || LastName is null || Phone is null);
        }

        #endregion
    }
}
