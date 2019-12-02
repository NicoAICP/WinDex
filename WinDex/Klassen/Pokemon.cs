
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WinDex
{
    class Pokemon
    {
        public  string Entry { get; set; }
        public  string Name { get; set; }
        public  bool N_Caught { get; set; }
        public  int N_Encounter { get; set; }
        public  bool S_Caught { get; set; }
        public  int S_Encounter { get; set; }
        public  string P_Sprite_Link { get; set; }
        public string N_Sprite { get; set; }

        public string N_Sprite_Link { get; set; }
        public string S_Sprite { get; set; }
        public string S_Sprite_Link { get; set; }
        public string P_Sprite { get; set; }
        public bool ncounter()
        {
            return N_Caught;
        }
        public bool scounter()
        {
            return S_Caught;
        }

        public Pokemon()
        {

        }
        public Pokemon(string entry, string name, bool n_caught, int n_encounter, bool s_caught, int s_encounter, string p_sprite, string n_sprite, string s_sprite)
        {
            Entry       =   entry;
            Name        =   name;
            N_Caught    =   n_caught;
            N_Encounter =   n_encounter;
            S_Caught    =   s_caught;
            S_Encounter =   s_encounter;
            P_Sprite_Link    =   p_sprite;
            N_Sprite_Link   =    n_sprite;
            S_Sprite_Link    =   s_sprite;
            #region S
            if ((!File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\{Name}.png") || !File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\{Name.Split(':')[0]}.png")) && S_Sprite_Link != "none")
            {
                if (!Directory.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\"))
                {
                    Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\");
                }
                string file = string.Empty;
                if (Name.Contains(":"))
                {
                    file = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\{Name.Split(':')[0]}.png";
                }
                else
                {
                    file = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\{Name}.png";
                }

                System.Net.WebClient cln = new System.Net.WebClient();
                cln.DownloadFile(S_Sprite_Link, file);

            }
            if (Name.Contains(":"))
            {
               S_Sprite = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\{Name.Split(':')[0]}.png";
            }
            else
            {
                S_Sprite = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\SSprites\{Name}.png";
            }
            #endregion
            #region N
            if ((!File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\{Name}.png") || !File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\{Name.Split(':')[0]}.png")) && N_Sprite_Link != "none")
            {
                if (!Directory.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\"))
                {
                    Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\");
                }
                string file = string.Empty;
                if (Name.Contains(":"))
                {
                    file = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\{Name.Split(':')[0]}.png";
                }
                else
                {
                    file = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\{Name}.png";
                }

                System.Net.WebClient cln = new System.Net.WebClient();
                cln.DownloadFile(N_Sprite_Link, file);

                
            }
            if (Name.Contains(":"))
            {
                N_Sprite = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\{Name.Split(':')[0]}.png";
            }
            else
            {
                N_Sprite = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\NSprites\{Name}.png";
            }
            #endregion
            #region P
            if ((!File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\{Name}.png") || !File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\{Name.Split(':')[0]}.png")) && P_Sprite_Link != "none")
            {
                if (!Directory.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\"))
                {
                    Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\");
                }
                string file = string.Empty;
                if (Name.Contains(":"))
                {
                    file = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\{Name.Split(':')[0]}.png";
                }
                else
                {
                    file = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\{Name}.png";
                }
                     
                    System.Net.WebClient cln = new System.Net.WebClient();
                    cln.DownloadFile(P_Sprite_Link, file);
                
            }
            if (Name.Contains(":"))
            {
                P_Sprite = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\{Name.Split(':')[0]}.png";
            }
            else
            {
                P_Sprite = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\Sprites\{Name}.png";
            }
            #endregion
           
            
        }

        public string toCSV()
        {
            string n = "0";
            string s = "0";
            if (N_Caught)
            {
                n = "1";
            }
            if (S_Caught)
            {
                s = "1";
            }
            return $"{Entry};{Name};{n};{N_Encounter};{s};{S_Encounter};{P_Sprite_Link};{N_Sprite_Link};{S_Sprite_Link}";
        }
        public override string ToString()
        {
            return $"{Entry} {Name}";
        }
        public List<string> export()
        {
            List<string> exp = new List<string>();
            string n = "0";
            string s = "0";
            if (N_Caught)
            {
                n = "1";
            }
            if (S_Caught)
            {
                s = "1";
            }
            exp.Add(Entry);
            exp.Add(n);
            exp.Add(N_Encounter.ToString());
            exp.Add(s);
            exp.Add(S_Encounter.ToString());
            return exp;
        }
       
    }
}
