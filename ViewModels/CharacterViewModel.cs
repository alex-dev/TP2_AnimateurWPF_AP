using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    class CharacterViewModel : INotifyPropertyChanged
    {
        #region Internal Data
        private Personnage Character { get; set; }
        private string _Name { get; set; }
        private int? _HitPoints { get; set; }
        private int? _DamagePoints { get; set; }
        private Race _Race { get; set; }
        private List<Ability> _Abilities { get; set; }
        #endregion

        #region Data

        public string Name
        {
            get { return Character?.Nom ?? _Name; }
            set
            {
                if (value is null || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Un nom ne peut pas être vide.");
                }
                else if (Character is null)
                {
                    _Name = value;
                }
                else
                {
                    Character.Nom = value;
                }

                OnPropertyChanged();
            }
        }
        public int? HitPoints
        {
            get { return Character?.PointsVie ?? _HitPoints; }
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("Les points de vie doivent avoir une valeur.");
                }
                if (Character is null)
                {
                    _HitPoints = value;
                }
                else
                {
                    Character.PointsVie = (int)value;
                }

                OnPropertyChanged();
            }
        }
        public int? DamagePoints
        {
            get { return Character?.PointsDommage ?? _DamagePoints; }
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("Les points de dommage doivent avoir une valeur.");
                }
                if (Character is null)
                {
                    _DamagePoints = value;
                }
                else
                {
                    Character.PointsDommage = (int)value;
                }

                OnPropertyChanged();
            }
        }
        public Race Race
        {
            get { return Character?.Race ?? Race; }
            set
            {
                if (Character is null)
                {
                    _Race = value;
                }
                else
                {
                    Character.Race = value;
                }

                OnPropertyChanged();
            }
        }
        //public ObservableCollection<Ability>

        #endregion


        #region Constructors

        /// <summary>Constructeur par défaut</summary>
        public CharacterViewModel() : this(null) { }

        /// <summary>Constructeur de base</summary>
        /// <param name="animator">Animateur lié au vue modèle</param>
        public CharacterViewModel(Personnage character)
        {
            Character = character;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Character is null && !(_Name is null || _HitPoints is null || _DamagePoints is null))
            {
                Character = new Personnage(_Name, (int)_HitPoints, (int)_DamagePoints, null, new ObservableCollection<Ability>());
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
