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
            List<string> list = null;
                
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
            List<string> list = null;

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
            List<string> list = null;

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
            List<string> list = null;

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
            string date = String.Format(DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            string temp = String.Format(name + ", " + sourcePath + ", " + destinationPath + ", " + date);
            try
            {
                if (File.Exists(ProgramDataPath))
                    File.AppendAllText(ProgramDataPath, string.Format("{1}{0}", temp, Environment.NewLine));
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
            File.Delete(ProgramDataPath);
        }
    }
}
