using System.Collections.ObjectModel;
using System.Windows;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP.Views
{
    /// <summary>
    /// Logique d'interaction pour AbilitiesListWindow.xaml
    /// </summary>
    public partial class AbilitiesListWindow : Window
    {
        public ObservableCollection<Ability> Abilities { get; set; }

        public AbilitiesListWindow(ObservableCollection<Ability> abilities)
        {
            Abilities = abilities;

            InitializeComponent();
        }

        private void BtnAbilityAdd_Click(object sender, RoutedEventArgs e)
        {
            Ability.Abilities.Add(new Ability(TxtAbilityAdd.Text));
        }
    }
}
