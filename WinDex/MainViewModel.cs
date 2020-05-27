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
                lPokemon.Add(new Pokemon(split[0], split[1], coughtn, Convert.ToInt32(split[3]), coughts, Convert.ToInt32(split[5])));
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
        public static void Download(Editions edition, SwSH sh)
        {
            switch (edition)
            {
                case Editions.swsh:
                    
                    CSV_Interpreter.Download(Editions.swsh);
                    sh.Close();
                    try
                    {
                        Directory.Delete(CSV_Interpreter.path + "SSprites", true);
                    }
                    finally
                    {
                        try
                        {
                            Directory.Delete(CSV_Interpreter.path + "NSprites", true);
                        }
                        finally
                        {
                            try
                            {
                                Directory.Delete(CSV_Interpreter.path + "Sprites", true);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                   
                    
                    
                    break;
            }
        }
        public void UpdateDB(SwSH sh)
        {
            MessageBox.Show("For safety purposes you will be asked to choose a location where your programm data is exported.");
            Ex_import.ExportAll(this);
            Download(this.edition, sh);
            MessageBox.Show("Please choose the FullBackup.wndx file you created earlier.");
            Ex_import.Import(this);
            MessageBox.Show("The programm will now restart and reinitialize after choosing an edition. This process may take up a few minutes.");
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }
        public static void CheckIfPathExists()
        {
            if (!Directory.Exists(CSV_Interpreter.path))
            {
                CSV_Interpreter.CreateFolder();
            }
        }
        public Pokemon search(string name)
        {
            Pokemon p = null;
            foreach(Pokemon pkmn in LPokemon)
            {
                if(pkmn.Name == name || pkmn.Entry == name)
                {
                    p = pkmn;
                    break;
                }
            }
            return p;
        }
    }
}
