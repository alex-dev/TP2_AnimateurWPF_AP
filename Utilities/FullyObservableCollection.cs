using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace TP2_AnimateursWPF_AP.Utilities
{
    /// <summary>
    ///     Implements an <see cref="ObservableCollection{T}"/> watching for <see cref="ItemPropertyChanged"/> events.
    /// </summary>
    /// <typeparam name="T">Collection item implementing <see cref="INotifyPropertyChanged"/>.</typeparam>
    /// <remarks>Source: https://stackoverflow.com/a/32013610 </remarks>
    public class FullyObservableCollection<T> : ObservableCollection<T>, INotifyItemPropertyChanged
        where T : INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>Default constructor.</summary>
        public FullyObservableCollection() : base()
        {
            CollectionChanged += CollectionChangedHandler;
        }

        /// <summary>Wrap <see cref="List{T}"/>.</summary>
        /// <param name="list"><see cref="List{T}"/> to wrap.</param>
        public FullyObservableCollection(List<T> list) : base(list)
        {
            CollectionChanged += CollectionChangedHandler;
            ObserveAll();
        }

        /// <summary>Wrap <see cref="IEnumerable{T}"/>.</summary>
        /// <param name="list"><see cref="IEnumerable{T}"/> to wrap.</param>
        public FullyObservableCollection(IEnumerable<T> enumerable) : base(enumerable)
        {
            CollectionChanged += CollectionChangedHandler;
            ObserveAll();
        }

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

        /// <summary>
        ///     Triggered by <see cref="CollectionChanged"/>, update items subscription.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove
                || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.OldItems)
                {
                    item.PropertyChanged -= ItemPropertyChangedHandler;
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Add
                || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.NewItems)
                {
                    item.PropertyChanged += ItemPropertyChangedHandler;
                }
            }
        }

        /// <summary>
        ///     Triggered by <see cref="T"/>'s <see cref="PropertyChanged"/>, triggers
        ///     <see cref="ItemPropertyChanged"/>.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ItemPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            int index = Items.IndexOf((T)sender);

            if (index < 0)
            {
                throw new ArgumentException("Received sender is not in collection.");
            }
            else
            {
                OnItemPropertyChanged(new ItemPropertyChangedEventArgs(index, e));
            }
        }

        #endregion

        #region Methods
        /// <summary>Clear items, making sure to unsubscribe.</summary>
        protected override void ClearItems()
        {
            foreach (var item in Items)
            {
                item.PropertyChanged -= ItemPropertyChangedHandler;
            }

            base.ClearItems();
        }

        /// <summary>Subscribe to all <see cref="T"/>'s <see cref="T.PropertyChanged"/>.</summary>
        private void ObserveAll()
        {
            foreach (var item in Items)
            {
                item.PropertyChanged += ItemPropertyChangedHandler;
            }
        }

        #endregion
    }
}
