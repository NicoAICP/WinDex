using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDex
{
    
    class CSV_Interpreter
    {
        public static string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\WinDex\";
        public static List<string> createDataSetFromCSV(Editions edition)
        {
            List<string> lDataSet = new List<string>();
            string fileName = string.Empty;
            switch (edition)
            {
                case Editions.swsh:
                    fileName = "swsh.csv";
                    break;
                default:
                    throw new Exception();
            }
            try
            {
                StreamReader sr = new StreamReader($"{path}{fileName}");
                var readLine = string.Empty;
                while((readLine = sr.ReadLine()) != null)
                {
                    lDataSet.Add(readLine);
                }
                sr.Close();
            }
            catch (Exception)
            {
                lDataSet.Clear();
            }
            return lDataSet;
        }
        public static void save(List<string> DataSet, Editions edition)
        {
            string fileName = string.Empty;
            switch (edition)
            {
                case Editions.swsh:
                    fileName = "swsh.csv";
                    break;
                default:
                    throw new Exception();
            }

            StreamWriter sw = new StreamWriter($"{path}{fileName}");
            foreach(string temp in DataSet)
            {
                sw.WriteLine(temp);
            }
            sw.Close();
        }
        public static void Download(Editions edition)
        {
            switch (edition)
            {
                case Editions.swsh:
                    System.Net.WebClient cln = new System.Net.WebClient();
                    cln.DownloadFile("https://cdn.discordapp.com/attachments/496027184734011416/646519667942490115/swsh.csv", $"{path}swsh.csv");
                    break;
            }
        }
        public static void CreateFolder()
        {
           
                Directory.CreateDirectory(CSV_Interpreter.path);
            
        }
    }
}
