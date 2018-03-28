using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TP2_AnimateursWPF_AP.ViewModels;

namespace TP2_AnimateursWPF_AP
{
    /// <summary>Logique d'interaction pour AnimatorsListWindow.xaml</summary>
    public partial class AnimatorsListWindow : Window
    {
        #region Constructors

        public AnimatorsListWindow()
        {
            InitializeComponent();
            ((AnimatorViewModel)Details.DataContext).PropertyChanged += Details_PropertyChanged;
        }

        #endregion

        #region Event Handlers

        private void DtgAnimators_SelectionChanged(object sender, EventArgs eventArgs)
        {
            if (((DataGrid)sender).CurrentItem is null)
            {
                Details.DataContext = new AnimatorViewModel();
                ((AnimatorViewModel)Details.DataContext).PropertyChanged += Details_PropertyChanged;
            }
            else
            {
                Details.DataContext = ((DataGrid)sender).CurrentItem;
            }
        }

        private void Details_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!((AnimatorsViewModel)DataContext).Animators.Contains((AnimatorViewModel)sender))
            {
                ((AnimatorsViewModel)DataContext).Add((AnimatorViewModel)Details.DataContext);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            ((AnimatorsViewModel)DataContext).Remove((AnimatorViewModel)DtgAnimators.SelectedItem);
        }

        #endregion
    }
}
