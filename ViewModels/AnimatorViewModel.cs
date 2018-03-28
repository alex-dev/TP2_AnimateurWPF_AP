using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    /// <summary>
    ///   Interface la vue à l'<see cref="Animateur"/> tout en implémentant <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class AnimatorViewModel : INotifyPropertyChanged
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
        public ReadOnlyCollection<Personnage> Characters
        {
            get { return new ReadOnlyCollection<Personnage>(Animator?.LstPersonnages); }
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

            if (!(Animator.LstPersonnages is ObservableCollection<Personnage>))
            {
                Animator.LstPersonnages = new ObservableCollection<Personnage>(Animator.LstPersonnages);
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Animator is null && !(_FirstName is null || _LastName is null || _Phone is null))
            {
                Animator = new Animateur(_FirstName, _LastName, _Phone);
                Animator.LstPersonnages = new ObservableCollection<Personnage>(Animator.LstPersonnages);
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
