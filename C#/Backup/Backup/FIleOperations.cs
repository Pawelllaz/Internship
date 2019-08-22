using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Backup
{
    class FIleOperations
    {
        private string ProgramDataPath;
        public
        FIleOperations(string path)
        {
            ProgramDataPath = path;
        }

        public List<string> ReadNames()
        {
            List<string> list = new List<string>();
                
            if (File.Exists(ProgramDataPath))
            {
                string[] strAll = File.ReadAllLines(ProgramDataPath);
                list = new List<string>();
                foreach (var item in strAll)
                {
                    string[] temp = item.Split(new[] { ", " }, StringSplitOptions.None);
                    list.Add(temp[0]);
                }
            }

            return list;
        }

        public List<string> ReadSourcePath()
        {
            List<string> list = new List<string>();

            if (File.Exists(ProgramDataPath))
            {
                string[] strAll = File.ReadAllLines(ProgramDataPath);
                list = new List<string>();
                foreach (var item in strAll)
                {
                    string[] temp = item.Split(new[] { ", " }, StringSplitOptions.None);
                    list.Add(temp[1]);
                }
            }

            return list;
        }

        public List<string> ReadDestinationPath()
        {
            List<string> list = new List<string>();

            if (File.Exists(ProgramDataPath))
            {
                string[] strAll = File.ReadAllLines(ProgramDataPath);
                list = new List<string>();
                foreach (var item in strAll)
                {
                    string[] temp = item.Split(new[] { ", " }, StringSplitOptions.None);
                    list.Add(temp[2]);
                }
            }

            return list;
        }

        public List<string> ReadDate()
        {
            List<string> list = new List<string>();

            if (File.Exists(ProgramDataPath))
            {
                string[] strAll = File.ReadAllLines(ProgramDataPath);
                list = new List<string>();
                foreach (var item in strAll)
                {
                    string[] temp = item.Split(new[] { ", " }, StringSplitOptions.None);
                    list.Add(temp[3]);
                }
            }

            return list;
        }

        public bool SaveRecord(string name, string sourcePath, string destinationPath)
        {
            string temp = String.Format(name + ", " + sourcePath + ", " + destinationPath + ", " + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + Environment.NewLine);
            try
            {
                if (File.Exists(ProgramDataPath))
                    File.AppendAllText(ProgramDataPath,temp);
                else
                    File.WriteAllText(ProgramDataPath, temp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }

        public void DeleteData()
        {
            if(File.Exists(ProgramDataPath))
                File.WriteAllText(ProgramDataPath,"");
        }
    }
}
