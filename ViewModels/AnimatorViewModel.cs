using System.ComponentModel;
using System.Runtime.CompilerServices;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.ViewModels
{
    class AnimatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Animateur Animator { get; set; }

        public string FirstName
        {
            get { return Animator.Prenom; }
            set
            {
                Animator.Prenom = value;
                OnPropertyChanged();
            }
        }
        public string LastName {
            get { return Animator.Nom; }
            set
            {
                Animator.Nom = value;
                OnPropertyChanged();
            }
        }
        public string Telephone {
            get { return Animator.Telephone.ToString(); }
            set
            {
                Animator.Telephone = new PhoneNumber(value);
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
