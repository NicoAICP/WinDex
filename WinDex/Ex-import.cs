using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinDex
{
    class Ex_import
    {
        private static byte[] EncryptString(byte[] clearText, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearText, 0, clearText.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }
        public static string EncryptString(string clearText, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] encryptedData = EncryptString(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }
        private static byte[] DecryptString(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }
        public static string DecryptString(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] decryptedData = DecryptString(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public static void ExportSingle(List<string> list)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                StreamWriter sw = new StreamWriter($"{ System.IO.Path.GetFullPath(dialog.SelectedPath)}/{list[0]}.wndx");
                sw.WriteLine(EncryptString($"{list[0]};{list[1]};{list[2]};{list[3]};{list[4]}","WinDex"));
                sw.Close();
            }
        }
        public static void ExportAll(MainViewModel mvm)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                StreamWriter sw = new StreamWriter($"{ System.IO.Path.GetFullPath(dialog.SelectedPath)}/FullBackup.wndx");
                List<Pokemon> pkmnL = mvm.LPokemon;
                foreach(Pokemon p in pkmnL)
                {
                    List<string> list = p.export();
                    sw.WriteLine(EncryptString($"{list[0]};{list[1]};{list[2]};{list[3]};{list[4]}", "WinDex"));
                }
                
                sw.Close();
            }
        }
        public static void Import(MainViewModel mvm)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.Filter = "WinDex Export files (*.wndx)|*.wndx";
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                
                StreamReader sr = new StreamReader(dialog.InitialDirectory + dialog.FileName);
                var read = string.Empty;
                List<string> input = new List<string>();
                while((read = sr.ReadLine()) != null)
                {
                    input.Add(DecryptString(read, "WinDex"));
                }
                foreach(string s in input)
                {
                    var split = s.Split(';');
                    foreach (Pokemon p in mvm.LPokemon)
                    {
                        if (p.Entry == split[0])
                        {
                            bool coughtn = false;
                            bool coughts = false;
                            if (split[1] == "1")
                            {
                                coughtn = true;
                            }
                            if (split[3] == "1")
                            {
                                coughts = true;
                            }
                            p.N_Caught = coughtn;
                            p.N_Encounter = Convert.ToInt32(split[2]);
                            p.S_Caught = coughts;
                            p.S_Encounter = Convert.ToInt32(split[4]);
                            break;
                        }
                    }
                }
                mvm.save();
                
            }
        }
    }
}
