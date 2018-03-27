using System.Collections.ObjectModel;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    class AnimatorsViewModel
    {
        private ObservableCollection<Animateur> _Animators { get; set; }
        public ReadOnlyObservableCollection<Animateur> Animators
        {
            get
            {
                return new ReadOnlyObservableCollection<Animateur>(_Animators);
            }
        }

        public AnimatorsViewModel()
        {
            _Animators = new ObservableCollection<Animateur>(Animateur.ChargerListeAnimateurs());
        }
    }
}
