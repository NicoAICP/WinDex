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
    /// Interaktionslogik für search.xaml
    /// </summary>
    public partial class search : Window
    {
        public search()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = (MainViewModel)Application.Current.FindResource("mvm");
            Pokemon p = mvm.search(tbName.Text);
            if(p == null)
            {
                MessageBox.Show($"The Pokémon with the Entry Number or Name {tbName.Text} was not found");
            }
            else
            {
                this.Close();
                mvm.SelectedPokemon = p;
                PKMN_Window pk = new PKMN_Window();
                pk.ShowDialog();
            }
        }
    }
}
