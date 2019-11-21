using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinDex
{
    public enum Editions
    {
        swsh
    }
    class MainViewModel
    {
        public List<Pokemon> LPokemon { get; set; } = new List<Pokemon>();
        public Pokemon SelectedPokemon;
        public Editions edition { get; set; }
        public MainViewModel()
        {
           
        }
        public void save()
        {
            List<string> pokemontocsv = new List<string>();
            foreach(Pokemon p in LPokemon)
            {
                pokemontocsv.Add(p.toCSV());
            }
            CSV_Interpreter.save(pokemontocsv, this.edition);
        }
        public void getList(Editions editions)
        {
            LPokemon =createPokemonFromDataSet(CSV_Interpreter.createDataSetFromCSV(editions));
        }
        private static List<Pokemon> createPokemonFromDataSet(List<string> DataSet)
        {
            List<Pokemon> lPokemon = new List<Pokemon>();
            foreach (string temp_string in DataSet)
            {
                var split = temp_string.Split(';');
                bool coughtn = false;
                bool coughts = false;
                if (split[2] == "1")
                {
                    coughtn = true;
                }
                if (split[4] == "1")
                {
                    coughts = true;
                }
                lPokemon.Add(new Pokemon(split[0], split[1], coughtn, Convert.ToInt32(split[3]), coughts, Convert.ToInt32(split[5]), split[6], split[7], split[8]));
            }

            return lPokemon;
        }
        public static void Sword()
        {
            DownloadIfNotExist(Editions.swsh);
            
            MainViewModel mvm = (MainViewModel)Application.Current.FindResource("mvm");
            mvm.getList(Editions.swsh);
            mvm.edition = Editions.swsh;
            SwSH sw = new SwSH();
            sw.Show();
            
        }
        
        public static void DownloadIfNotExist(Editions edition)
        {
            switch (edition)
            {
                case Editions.swsh:
                    if (!File.Exists($"{CSV_Interpreter.path}swsh.csv"))
                    {
                        CSV_Interpreter.Download(Editions.swsh);
                    }
                    break;
            }
        }
        public static void CheckIfPathExists()
        {
            if (!Directory.Exists(CSV_Interpreter.path))
            {
                CSV_Interpreter.CreateFolder();
            }
        }
    }
}
