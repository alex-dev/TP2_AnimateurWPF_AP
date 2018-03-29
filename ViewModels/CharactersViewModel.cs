using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    /// <summary>Vue modèle gérant un ensemble d'animateurs via <see cref="CharacterViewModel"/>.</summary>
    public class CharactersViewModel : INotifyCollectionChanged
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

        #region Constructors

        /// <summary>Constructeur quand les données sont connues.</summary>
        public CharactersViewModel(IEnumerable<Personnage> data)
        {
            _Characters = new ObservableCollection<CharacterViewModel>(
                from character in data
                select new CharacterViewModel(character));
        }

        public CharactersViewModel()
        {
            _Characters = new ObservableCollection<CharacterViewModel>();
        }

        #endregion

        #region Methods

        /// <summary>Ajoute un animateur à la liste.</summary>
        /// <param name="animator">L'animateur à ajouter</param>
        /// <exception cref="ArgumentException"><paramref name="animator"/> n'est pas valide.</exception>
        public void Add(CharacterViewModel character)
        {
            _Characters.Add(character);
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

        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { _Characters.CollectionChanged += value; }
            remove { _Characters.CollectionChanged -= value; }
        }

        #endregion
    }
}
