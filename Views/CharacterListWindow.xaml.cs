using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP2_AnimateursWPF_AP.ViewModels;

namespace TP2_AnimateursWPF_AP.Views
{
    /// <summary>
    /// Logique d'interaction pour CharacterListWindow.xaml
    /// </summary>
    public partial class CharacterListWindow : Window
    {
        #region Constructors

        public CharacterListWindow()
        {
            InitializeComponent();
            ((CharacterViewModel)Details.DataContext).PropertyChanged += Details_PropertyChanged;
        }


        public CharacterListWindow(CharactersViewModel characters) : this()
        {
            DataContext = characters;
        }

        #endregion

        #region Event Handlers

        private void DtgCharacters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DataGrid)sender).SelectedItem is null)
            {
                Details.DataContext = new CharacterViewModel();
                ((CharacterViewModel)Details.DataContext).PropertyChanged += Details_PropertyChanged;
            }
            else
            {
                Details.DataContext = ((DataGrid)sender).SelectedItem;
            }
        }

        private void DtgCharacters_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DtgCharacters.SelectedItem = null;
        }

        private void Details_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!((CharactersViewModel)DataContext).Characters.Contains((CharacterViewModel)sender))
            {
                ((CharactersViewModel)DataContext).Add((CharacterViewModel)Details.DataContext);
                DtgCharacters.SelectedItem = (CharacterViewModel)Details.DataContext;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            ((CharactersViewModel)DataContext).Remove((CharacterViewModel)DtgCharacters.SelectedItem);
        }

        #endregion
    }
}
