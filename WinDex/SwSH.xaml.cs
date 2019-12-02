using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WinDex
{
    /// <summary>
    /// Interaktionslogik für SwSH.xaml
    /// </summary>
    public partial class SwSH : Window
    {
        public SwSH()
        {
            InitializeComponent();
        }

        private void lvLPKMN_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if((sender as ListBox).SelectedIndex > -1)
            {
                MainViewModel mvm = (MainViewModel)Application.Current.FindResource("mvm");
                mvm.SelectedPokemon = (Pokemon)(sender as ListBox).SelectedItem;
                PKMN_Window pkmn = new PKMN_Window();
                pkmn.ShowDialog();
            }
        }


        private void miIMP_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = (MainViewModel)Application.Current.FindResource("mvm");
            Ex_import.Import(mvm);
        }

        private void miEXPA_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = (MainViewModel)Application.Current.FindResource("mvm");
            Ex_import.ExportAll(mvm);
        }
    }
}
