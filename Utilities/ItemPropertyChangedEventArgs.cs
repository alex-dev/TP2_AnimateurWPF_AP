﻿using System.ComponentModel;

namespace TP2_AnimateursWPF_AP.Utilities
{
    /// <summary>Provides data for the <see cref="FullyObservableCollection{T}.ItemPropertyChanged"/> event.</summary>
    public class ItemPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>Index in the collection for which the property change has occurred.</summary>
        public int CollectionIndex { get; }

        /// <summary>Initializes a new instance of the <see cref="ItemPropertyChangedEventArgs"/> class.</summary>
        /// <param name="index">The index in the collection of changed item.</param>
        /// <param name="name">The name of the property that changed.</param>
        public ItemPropertyChangedEventArgs(int index, string name)
            : base(name)
        {
            CollectionIndex = index;
        }

        /// <summary>Initializes a new instance of the <see cref="ItemPropertyChangedEventArgs"/> class.</summary>
        /// <param name="index">The index.</param>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        public ItemPropertyChangedEventArgs(int index, PropertyChangedEventArgs args)
            : this(index, args.PropertyName) { }
    }
}
