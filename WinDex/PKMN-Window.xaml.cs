using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaktionslogik für PKMN_Window.xaml
    /// </summary>
    public partial class PKMN_Window : Window
    {
        MainViewModel mvm = (MainViewModel)Application.Current.FindResource("mvm");
        bool s = false;
        public PKMN_Window()
        {
            InitializeComponent();
            this.Title = $"Pokémon: {mvm.SelectedPokemon.Name}";
            eS.Text = mvm.SelectedPokemon.S_Encounter.ToString();
            eNS.Text = mvm.SelectedPokemon.N_Encounter.ToString();
            Entry.Text = mvm.SelectedPokemon.Entry;
            cN.IsChecked = mvm.SelectedPokemon.N_Caught;
            cSH.IsChecked = mvm.SelectedPokemon.S_Caught;
            if(mvm.SelectedPokemon.S_Sprite != "none" && mvm.SelectedPokemon.S_Sprite_Link != "none")
            {
                Sprite.Source = new BitmapImage(new Uri(mvm.SelectedPokemon.S_Sprite));
            } else if(mvm.SelectedPokemon.N_Sprite != "none" && mvm.SelectedPokemon.N_Sprite_Link != "none")
            {
                Sprite.Source = new BitmapImage(new Uri(mvm.SelectedPokemon.N_Sprite));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mvm.SelectedPokemon.S_Encounter = Convert.ToInt32(eS.Text);
            mvm.SelectedPokemon.N_Encounter = Convert.ToInt32(eNS.Text);
            if (cN.IsChecked == true)
            {
                mvm.SelectedPokemon.N_Caught = true;
            }
            else
            {
                mvm.SelectedPokemon.N_Caught = false;
            }
            if (cSH.IsChecked == true)
            {
                mvm.SelectedPokemon.S_Caught = true;
            }
            else
            {
                mvm.SelectedPokemon.S_Caught = false;
            }

            mvm.save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            eNS.Text = (Convert.ToInt32(eNS.Text) + 1).ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(eNS.Text) > 0)
            {
                eNS.Text = (Convert.ToInt32(eNS.Text) - 1).ToString();
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            eS.Text = (Convert.ToInt32(eS.Text) + 1).ToString();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {   if(Convert.ToInt32(eS.Text) > 0)
            {
                eS.Text = (Convert.ToInt32(eS.Text) - 1).ToString();
            }
            
        }

        private void Sprite_MouseEnter(object sender, MouseEventArgs e)
        {
            if (s)
            {
                Sprite.Source = new BitmapImage(new Uri(mvm.SelectedPokemon.S_Sprite));
            }
            else
            {
                Sprite.Source = new BitmapImage(new Uri(mvm.SelectedPokemon.N_Sprite));
            }
        }

        private void Sprite_MouseLeave(object sender, MouseEventArgs e)
        {
            if (s)
            {
                Sprite.Source = new BitmapImage(new Uri(mvm.SelectedPokemon.N_Sprite));
            }
            else
            {
                Sprite.Source = new BitmapImage(new Uri(mvm.SelectedPokemon.S_Sprite));
            }
        }
    }
}
