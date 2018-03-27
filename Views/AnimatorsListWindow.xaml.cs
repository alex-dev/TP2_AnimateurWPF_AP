using System.Windows;
using System.Windows.Controls;
using TP2_AnimateursWPF_AP.Models;

namespace TP2_AnimateursWPF_AP
{
    /// <summary>
    /// Logique d'interaction pour AnimatorsListWindow.xaml
    /// </summary>
    public partial class AnimatorsListWindow : Window
    {
        public AnimatorsListWindow()
        {
            InitializeComponent();
        }

        private void AnimatorsDataGrid_SelectionChanged(object sender, RoutedEventArgs eventArgs)
        {
            var origin = eventArgs.OriginalSource as DataGrid;
            MessageBox.Show((origin.SelectedItem is Animateur).ToString());
        }
    }
}
