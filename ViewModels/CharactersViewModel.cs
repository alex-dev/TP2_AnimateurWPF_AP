using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    /// <summary>Vue modèle gérant un ensemble d'animateurs via <see cref="CharacterViewModel"/>.</summary>
    public class CharactersViewModel
    {
        #region Internal Data
        private ObservableCollection<CharacterViewModel> _Characters { get; set; }
        #endregion

        #region Data

        public ReadOnlyObservableCollection<CharacterViewModel> Characters
        {
            get { return new ReadOnlyObservableCollection<CharacterViewModel>(_Characters); }
        }

        #endregion

        #region Events Forwarding

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { _Characters.CollectionChanged += value; }
            remove { _Characters.CollectionChanged -= value; }
        }

        #endregion

        #region Constructors

        /// <summary>Constructeur par défaut</summary>
        public CharactersViewModel(IEnumerable<Personnage> data)
        {
            _Characters = new ObservableCollection<CharacterViewModel>(
                from character in data
                select new CharacterViewModel(character));
        }

        #endregion

        #region Methods

        /// <summary>Ajoute un animateur à la liste.</summary>
        /// <param name="animator">L'animateur à ajouter</param>
        /// <exception cref="ArgumentException"><paramref name="animator"/> n'est pas valide.</exception>
        public void Add(CharacterViewModel animator)
        {
            /*if (animator.Characters is null || animator.FirstName is null
                || animator.LastName is null || animator.Phone is null)
            {
                throw new ArgumentException("Cet animateur n'est pas valide.");
            }
            else
            {
                _Animators.Add(animator);
            }*/
        }

        /// <summary>Ajoute un animateur à la liste.</summary>
        /// <param name="animator">L'animateur à ajouter</param>
        public void Add(Personnage character)
        {
            _Characters.Add(new CharacterViewModel(character));
        }

        /// <summary>Retire un animateur de la liste.</summary>
        /// <param name="animator">L'animateur à retirer</param>
        public void Remove(CharacterViewModel character)
        {
            _Characters.Remove(character);
        }

        #endregion
    }
}
