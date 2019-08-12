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
        private string directoryPath;
        public
        FIleOperations(string path)
        {
            directoryPath = path;
        }

        public List<string> ReadNames()
        {
            List<string> list = null;
                
            if (File.Exists(directoryPath))
            {
                string[] strAll = File.ReadAllLines(directoryPath);
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
            List<string> list = null;

            if (File.Exists(directoryPath))
            {
                string[] strAll = File.ReadAllLines(directoryPath);
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
            List<string> list = null;

            if (File.Exists(directoryPath))
            {
                string[] strAll = File.ReadAllLines(directoryPath);
                list = new List<string>();
                foreach (var item in strAll)
                {
                    string[] temp = item.Split(new[] { ", " }, StringSplitOptions.None);
                    list.Add(temp[2]);
                }
            }

            return list;
        }

        public bool SaveRecord(string name, string sourcePath, string destinationPath)
        {
            string temp = String.Format(name + ", " + sourcePath + ", " + destinationPath);
            try
            {
                if (File.Exists(directoryPath))
                    File.AppendAllText(directoryPath, string.Format("{1}{0}", temp, Environment.NewLine));
                else
                    File.WriteAllText(directoryPath, temp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }
    }
}
