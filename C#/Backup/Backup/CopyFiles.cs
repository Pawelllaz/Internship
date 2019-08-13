using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup
{
    class CopyFiles
    {
        public long maxLength;
        public long currentLength;

        CopyFiles()
        {
            maxLength = 0;
            currentLength = 0;
        }

        public void Worker()
        {
            //???????
        }

        public void DirectoryCopy(string sourcerDirPath, string destDirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcerDirPath);
            if (!dir.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourcerDirPath);

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirPath))
                Directory.CreateDirectory(destDirPath);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                try
                {
                    FileInfo destFile = new FileInfo(Path.Combine(destDirPath, file.Name));
                    if (destFile.Exists)
                    {
                        if (file.LastWriteTime > destFile.LastWriteTime)
                        {
                            FileInfo f = file.CopyTo(destFile.FullName, true);
                            currentLength += f.Length;
                            Console.WriteLine(currentLength + " / " + maxLength);
                        }
                    }
                    else
                    {
                        FileInfo f = file.CopyTo(destFile.FullName, true);
                        currentLength += f.Length;
                        Console.WriteLine(currentLength * 100 / maxLength + "%");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Yo there is a problem: ", e.ToString());
                }
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirPath, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }

        public static long CalculateDirectorySize(DirectoryInfo directory, bool includeSubdirectories)
        {
            long totalSize = 0;

            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                totalSize += file.Length;
            }

            if (includeSubdirectories)
            {
                DirectoryInfo[] dirs = directory.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    totalSize += CalculateDirectorySize(dir, true);
                }
            }

            return totalSize;
        }
    }
}
