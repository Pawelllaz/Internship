using System;
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
        public void WriteNoteText(string title, string textToWrite)
        {
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
            try
            {
                textReaded = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Reading file error: {0}", e.ToString());
            }
            return textReaded;
        }

        public string[] ReadFilesPaths()
        {
            string[] files = null;
            if (Directory.Exists(directoryPath))
            {
                files = Directory.GetFiles(directoryPath);
            }
            return files;
        }

        private string MakeFilePath(string title)
        {
            string filePath = directoryPath;
            filePath += "\\" + title + ".txt";
            return filePath;
        }
    }
}
