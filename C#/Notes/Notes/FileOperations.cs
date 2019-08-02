using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Notes
{
    public class FileOperations
    {
        private readonly string directoryPath;

        public FileOperations(string newDirectoryPath)
        {
            directoryPath = newDirectoryPath;
        }

        /// <summary>
        /// Writing note text to file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="textToWrite"></param>
        public void WriteNoteText(string title, string textToWrite, string date, string time)
        {
            WriteDateHelper(title, date, time);
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    File.WriteAllText(MakeFilePath(title), textToWrite);
                }
                else
                {
                    Console.WriteLine("Directory does't exists");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Saving file error: {0}", e.ToString());
            }

        }

        private void WriteDateHelper(string title, string date, string time)
        {
            string dateToWrite = null;

            if (!String.IsNullOrEmpty(date) && !String.IsNullOrEmpty(time))
                dateToWrite = string.Format(date + ", " + time);
            else if (!String.IsNullOrEmpty(time))
                dateToWrite = string.Format(time);
            else if (!String.IsNullOrEmpty(date))
                dateToWrite = string.Format(date);

            if (!String.IsNullOrEmpty(dateToWrite))
            {
                try
                {
                    if (Directory.Exists(directoryPath))
                    {
                        File.WriteAllText(MakeDateFilePath(title), dateToWrite);
                    }
                    else
                    {
                        Console.WriteLine("Directory does't exists");
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("Saving file error: {0}", e.ToString());
                }
            }
        }

        /// <summary>
        /// creating new directory
        /// </summary>
        /// <returns></returns>
        public bool CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Creating directory fail: {0}", e.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// reading text from file to string
        /// </summary>
        /// <param name="path"></param>
        /// <returns>note text</returns>
        public string ReadTextFromFile(string path)
        {
            string textReaded = null;
            if (File.Exists(path))
            {
                try
                {
                    textReaded = File.ReadAllText(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading file error: {0}", e.ToString());
                }
            }

            string dateReaded = ReadTimeFromFile(path);
            if (dateReaded != null)
            {
                textReaded += string.Format("\n\nRemind at:\n" + dateReaded);
            }

            return textReaded;
        }

        public string ReadTextFromFileWithoutDate(string path)
        {
            string textReaded = null;
            if (File.Exists(path))
            {
                try
                {
                    textReaded = File.ReadAllText(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading file error: {0}", e.ToString());
                }
            }

            return textReaded;
        }

        public List<string> ReadFilesPaths()
        {
            List<string> listOfFiles = new List<string>();
            string[] files = null;
            if (Directory.Exists(directoryPath))
            {
                files = Directory.GetFiles(directoryPath);
            }
            foreach (string filePath in files)
            {
                if (!filePath.Contains("_TIME"))
                {
                    listOfFiles.Add(filePath);
                }
            }
            return listOfFiles;
        }

        public void RemoveTimeFile(string pathToNoteText)
        {
            File.Delete(GetPathToNoteTime(pathToNoteText));
        }

        private string ReadTimeFromFile(string path)
        {
            string dateReaded = null;
            string pathToTimeFile = GetPathToNoteTime(path);

            if (File.Exists(pathToTimeFile))
            {
                try
                {
                    dateReaded = File.ReadAllText(pathToTimeFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading file error: {0}", e.ToString());
                }
            }
            return dateReaded;
        }

        

        private string MakeFilePath(string title)
        {
            string filePath = directoryPath;
            filePath += "/" + title + ".txt";
            return filePath;
        }

        private string MakeDateFilePath(string title)
        {
            string filePath = directoryPath;
            filePath += "/" + title + "_TIME.txt";
            return filePath;
        }

        private string GetPathToNoteTime(string pathToNoteText)
        {
            return string.Format(Path.GetDirectoryName(pathToNoteText) + "\\" + Path.GetFileNameWithoutExtension(pathToNoteText) + "_TIME.txt");
        }
    }
}
