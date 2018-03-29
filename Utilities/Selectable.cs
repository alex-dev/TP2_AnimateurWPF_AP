using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TP2_AnimateursWPF_AP.Utilities
{
    public class Selectable<T> : INotifyPropertyChanged
    {
        private bool isSelected;

        public T Item { get; private set; }
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public Selectable(T item, bool isSelected = false)
        {
            Item = item;
            IsSelected = isSelected;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!(PropertyChanged is null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
