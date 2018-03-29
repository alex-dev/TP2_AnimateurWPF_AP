using System.Windows.Controls;
using TP2_AnimateursWPF_AP.ViewModels;

namespace TP2_AnimateursWPF_AP.Views
{
    /// <summary>Logique d'interaction pour AnimatorDetails.xaml</summary>
    public partial class AnimatorDetails : UserControl
    {
        #region Constructors

        public AnimatorDetails()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void BtnViewCharacters_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = ((AnimatorViewModel)DataContext).CreateCharactersViewModel();
            new CharacterListWindow(data.Item1).ShowDialog();
            data.Item2(data.Item1);
        }

        #endregion
    }
}
