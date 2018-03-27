using System;

namespace TP2_AnimateursWPF_AP.Utilities
{
    public interface INotifyItemPropertyChanged
    {
        event EventHandler<ItemPropertyChangedEventArgs> ItemPropertyChanged;
    }
}
