using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using TP2_AnimateursWPF_AP.Models;
using TP2_AnimateursWPF_AP.Utilities;
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

        public ObservableCollection<Selectable<Ability>> AllAbilities { get; private set; }
        public ObservableCollection<Selectable<Race>> AllRaces { get; private set; }

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
            set
            {
                if (Character is null)
                {
                    _Abilities = value.ToList();
                }
                else
                {
                    Character.LstHabiletes = value.ToList();
                }

                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>Constructeur par défaut</summary>
        public CharacterViewModel() : this(null) { }

        /// <summary>Constructeur de base</summary>
        /// <param name="animator">Animateur lié au vue modèle</param>
        public CharacterViewModel(Personnage character)
        {
            Character = character;

            {
                AllAbilities = new ObservableCollection<Selectable<Ability>>(
                    from ability in Ability.Abilities
                    select new Selectable<Ability>(ability, Abilities.Contains(ability)));
                WeakEventManager<ObservableCollection<Ability>, NotifyCollectionChangedEventArgs>
                    .AddHandler(Ability.Abilities, "CollectionChanged", Abilities_CollectionChanged);
            }
            {
                AllRaces = new ObservableCollection<Selectable<Race>>(
                    from race in Race.Races
                    select new Selectable<Race>(race, Race == race));
                WeakEventManager<ObservableCollection<Race>, NotifyCollectionChangedEventArgs>
                    .AddHandler(Race.Races, "CollectionChanged", Races_CollectionChanged);
            }
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

        #region Event Handlers

        /// <summary><see cref="ObservableCollection{Ability}.CollectionChanged"/> handler.</summary>
        public void Abilities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (Ability ability in e.OldItems)
            {
                AllAbilities.Remove(AllAbilities.Single(item => ability == item.Item));
            }
            foreach (Ability ability in e.NewItems)
            {
                AllAbilities.Add(new Selectable<Ability>(ability));
            }
        }

        /// <summary><see cref="ObservableCollection{Race}.CollectionChanged"/> handler.</summary>
        public void Races_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (Race race in e.OldItems)
            {
                AllRaces.Remove(AllRaces.Single(item => race == item.Item));
            }
            foreach (Race race in e.NewItems)
            {
                AllRaces.Add(new Selectable<Race>(race));
            }
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

            if (!(PropertyChanged is null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
