using System;
using System.Windows;
using TP2_AnimateursWPF_AP.ViewModels;

namespace TP2_AnimateursWPF_AP.Views
{
    /// <summary>
    /// Logique d'interaction pour CharacterListWindow.xaml
    /// </summary>
    public partial class CharacterListWindow : Window
    {
        public CharacterListWindow(CharactersViewModel characters)
        {
            DataContext = characters;
            InitializeComponent();
        }

        private void DtgCharacters_SelectionChanged(object sender, EventArgs eventArgs)
        {
            //Details.DataContext = ((DataGrid)sender).CurrentItem ?? new AnimatorViewModel();
        }
    }
}
