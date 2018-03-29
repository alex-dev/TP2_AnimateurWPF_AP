using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using TP2_AnimateursWPF_AP.Models;
using TP2_AnimateursWPF_AP.Validators;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    public class CharacterViewModel : INotifyPropertyChanged, IValidable
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

        public HashSet<Ability> AllAbilities { get; private set; }
        public HashSet<Race> AllRaces { get; private set; }

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
            get { return Character?.Race ?? _Race; }
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
        public ICollection<Ability> Abilities
        {
            get { return Character?.LstHabiletes ?? _Abilities ?? (_Abilities = new List<Ability>()); }
        }

        #endregion

        #region Constructors

        /// <summary>Constructeur par défaut</summary>
        public CharacterViewModel() : this(null) { }

        /// <summary>Constructeur de base</summary>
        /// <param name="animator">Animateur lié au vue modèle</param>
        public CharacterViewModel(Personnage character)
        {
            AllAbilities = Ability.Abilities;
            AllRaces = Race.Races;
            Character = character;
        }

        #endregion

        #region Methods

        public Personnage Extract()
        {
            return IsValid(null)
                ? Character
                : throw new InvalidOperationException("Character is in an invalid state.");
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Character is null && IsValid(null))
            {
                Character = new Personnage(_Name, (int)_HitPoints, (int)_DamagePoints, _Race, new List<Ability>());
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IValidable

        public bool IsValid(CultureInfo culture)
        {
            return !(string.IsNullOrWhiteSpace(Name) || HitPoints is null || DamagePoints is null || Race is null);
        }

        #endregion
    }
}
