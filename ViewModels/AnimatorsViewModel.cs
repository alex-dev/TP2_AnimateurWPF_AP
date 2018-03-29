using System.Collections.ObjectModel;
using System.Linq;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    /// <summary>Vue modèle gérant un ensemble d'animateurs via <see cref="AnimatorViewModel"/>.</summary>
    public class AnimatorsViewModel
    {
        #region Internal Data
        private ObservableCollection<AnimatorViewModel> _Animators { get; set; }
        #endregion

        #region Data

        public ReadOnlyObservableCollection<AnimatorViewModel> Animators
        {
            get { return new ReadOnlyObservableCollection<AnimatorViewModel>(_Animators); }
        }

        #endregion

        #region Constructors

        /// <summary>Constructeur par défaut</summary>
        public AnimatorsViewModel()
        {
            _Animators = new ObservableCollection<AnimatorViewModel>(
                from animator in Animateur.ChargerListeAnimateurs()
                select new AnimatorViewModel(animator));
        }

        #endregion

        #region Methods

        /// <summary>Ajoute un animateur à la liste.</summary>
        /// <param name="animator">L'animateur à ajouter</param>
        public void Add(AnimatorViewModel animator)
        {
            _Animators.Add(animator);
        }

        /// <summary>Ajoute un animateur à la liste.</summary>
        /// <param name="animator">L'animateur à ajouter</param>
        public void Add(Animateur animator)
        {
            _Animators.Add(new AnimatorViewModel(animator));
        }

        /// <summary>Retire un animateur de la liste.</summary>
        /// <param name="animator">L'animateur à retirer</param>
        public void Remove(AnimatorViewModel animator)
        {
            _Animators.Remove(animator);
        }

        #endregion
    }
}
