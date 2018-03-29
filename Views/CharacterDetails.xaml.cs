using System.Linq;
using System.Windows.Controls;
using TP2_AnimateursWPF_AP.Models;
using TP2_AnimateursWPF_AP.Utilities;
using TP2_AnimateursWPF_AP.ViewModels;

namespace TP2_AnimateursWPF_AP.Views
{
    /// <summary>Logique d'interaction pour CharacterDetails.xaml</summary>
    public partial class CharacterDetails : UserControl
    {
        #region Constructors

        public CharacterDetails()
        {
            InitializeComponent();
        }

        #endregion

        private void LstRace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(((ListBox)sender).SelectedItem is null))
            {
                ((CharacterViewModel)DataContext).Race = ((Selectable<Race>)((ListBox)sender).SelectedItem).Item;
            }
        }

        private void LstAbilities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(((ListBox)sender).SelectedItem is null))
            {
                ((CharacterViewModel)DataContext).Abilities = (from Selectable<Ability> selectable in ((ListBox)sender).SelectedItems
                                                               select selectable.Item).ToList();
            }
        }

        private void BtnAbilityAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Ability.Abilities.Add(new Ability(TxtAbilityAdd.Text));
        }
    }
}
