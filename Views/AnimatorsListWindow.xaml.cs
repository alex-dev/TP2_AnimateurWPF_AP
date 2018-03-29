using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_AnimateursWPF_AP.Models;
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
            ((AnimatorsViewModel)DataContext).CollectionChanged += ((App)Application.Current).Application_DetectedChange;
            ((AnimatorViewModel)Details.DataContext).PropertyChanged += Details_PropertyChanged;
        }

        #endregion

        #region Methods

        private void Save()
        {
            ((App)Application.Current).SaveChanges(
                from animator in ((AnimatorsViewModel)DataContext).Animators
                where animator.IsValid(null)
                select animator.Extract(),
                Ability.Abilities,
                Race.Races);
        }

        #endregion

        #region Event Handlers

        private void DtgAnimators_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGrid)sender).SelectedItem is null)
            {
                Details.DataContext = new AnimatorViewModel();
                ((AnimatorViewModel)Details.DataContext).PropertyChanged += Details_PropertyChanged;
            }
            else
            {
                Details.DataContext = ((DataGrid)sender).SelectedItem;
            }
        }

        private void DtgAnimators_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DtgAnimators.SelectedItem = null;
        }

        private void Details_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!((AnimatorsViewModel)DataContext).Animators.Contains((AnimatorViewModel)sender))
            {
                ((AnimatorsViewModel)DataContext).Add((AnimatorViewModel)Details.DataContext);
                DtgAnimators.SelectedItem = (AnimatorViewModel)Details.DataContext;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            ((AnimatorsViewModel)DataContext).Remove((AnimatorViewModel)DtgAnimators.SelectedItem);
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            const string message = "Voulez-vous enregistrer vos modifications?";
            const string title = "Sauvegarde";

            if (((App)Application.Current).IsDirty
                && MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Save();
            }
        }

        #endregion
    }
}
