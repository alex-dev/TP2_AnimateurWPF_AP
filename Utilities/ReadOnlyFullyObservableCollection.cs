using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace TP2_AnimateursWPF_AP.Utilities
{
    /// <summary>
    ///     Implements an <see cref="ReadOnlyObservableCollection{T}"/> watching for <see cref="ItemPropertyChanged"/> events.
    /// </summary>
    /// <typeparam name="T">Collection item implementing <see cref="INotifyPropertyChanged"/>.</typeparam>
    /// <remarks>Source: https://stackoverflow.com/a/32013610 </remarks>
    public class ReadOnlyFullyObservableCollection<T> : ReadOnlyObservableCollection<T>, INotifyItemPropertyChanged
        where T : INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>Wrap <see cref="List{T}"/>.</summary>
        /// <param name="list"><see cref="List{T}"/> to wrap.</param>
        public ReadOnlyFullyObservableCollection(FullyObservableCollection<T> list) : base(list) { }

        #endregion

        #region INotifyPropertyChanged

        /// <summary>Occurs when a property is changed within an item.</summary>
        public event EventHandler<ItemPropertyChangedEventArgs> ItemPropertyChanged;

        /// <summary>Invoke <see cref="ItemPropertyChanged"/>.</summary>
        /// <param name="e">Event arguments.</param>
        protected void OnItemPropertyChanged(ItemPropertyChangedEventArgs e)
        {
            ItemPropertyChanged?.Invoke(this, e);
        }

        #endregion

        #region Event Handlers

        /// <summary>Forward event to our listeners.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemPropertyChangedHandler(object sender, ItemPropertyChangedEventArgs e)
        {
            OnItemPropertyChanged(e);
        }

        #endregion
    }
}
