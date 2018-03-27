using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        }

        #endregion

        #region Event Handlers

        private void DtgAnimators_SelectionChanged(object sender, EventArgs eventArgs)
        {
            Details.DataContext = ((DataGrid)sender).CurrentItem ?? new AnimatorViewModel();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DtgAnimators.SelectedItem is null)
            {
                try
                {
                    ((AnimatorsViewModel)DataContext).Add((AnimatorViewModel)Details.DataContext);
                    // TODO: Find a way to select the row added.
                }
                catch (ArgumentException) { }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            ((AnimatorsViewModel)DataContext).Remove((AnimatorViewModel)DtgAnimators.SelectedItem);
        }

        #endregion
    }
}
