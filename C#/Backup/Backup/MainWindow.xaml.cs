using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.Windows.Forms;

namespace Backup
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<string> listOfBackupNames = new List<string>();
        private string _delay;
        public string Delay
        {
            get
            {
                return _delay;
            }
            set
            {
                _delay = value;
                OnPropertyChanged(nameof(Delay));
            }
        }
        private string _newBackupSourcePath;
        public string NewBackupSourcePath
        {
            get
            {
                return _newBackupSourcePath;
            }
            set
            {
                _newBackupSourcePath = value;
                OnPropertyChanged(nameof(NewBackupSourcePath));
            }
        }
        private string _newBackupDestinationPath;
        public string NewBackupDestinationPath
        {
            get
            {
                return _newBackupDestinationPath;
            }
            set
            {
                _newBackupDestinationPath = value;
                OnPropertyChanged(nameof(NewBackupDestinationPath));
            }
        }
        private string _backupName;
        public string BackupName
        {
            get
            {
                return _backupName;
            }
            set
            {
                _backupName = value;
                OnPropertyChanged(nameof(BackupName));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /////////==============>>> BUTTON CLICKS <<<==============//////////
        private void NewBackup_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BackupName))
            {
                BackupNameWarningAnimation();
            }
            else
            {
                //newbackuppopupanimation("0:0:0.4");
                //newbackuppopup.isopen = true;
                test_host.IsOpen = true;
            }
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //NewBackupPopupReverseAnimation("0:0:0.4");
            //newBackupPopup.IsOpen = false;
            test_host.IsOpen = false;
        }

        private void MakeNewBackup_Click(object sender, RoutedEventArgs e)
        {
            DirectoryCopy(NewBackupSourcePath, NewBackupDestinationPath);
            test_host.IsOpen = false;

            // it need to be repaired

            string[] text = ReadFile(@"C:\kopia.txt");
            
            if(!listOfBackupNames.Contains(BackupName))
            {
                listOfBackupNames.Add(BackupName);
                if (!text.Contains(BackupName))
                {
                    string[] stringToWrite = null;
                    stringToWrite[0] = String.Format(BackupName + ", " + NewBackupSourcePath + ", " + NewBackupDestinationPath);
                    if (!File.Exists(@"C:\kopia.txt"))
                    {
                        File.WriteAllLines(@"C:\kopia.txt", stringToWrite);
                    }
                    else
                        File.AppendAllLines(@"C:\kopia.txt", stringToWrite);
                }
            }
        }

        private string[] ReadFile(string path)
        {
            string[] text = null;

            try
            {
                text = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return text;
        }

        private void BackupSourceTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewBackupSourcePath = OpenFolderBrowser("0:0:0.2");
        }

        private void BackupDestinationTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewBackupDestinationPath = OpenFolderBrowser("0:0:0.2");
        }

        /////////==============>>> ANIMATIONS <<<==============//////////
        private void NewBackupPopupAnimation(string delay)
        {
            Delay = delay;
            var resource = myWindow.Resources["NewBackupAnimation"] as Storyboard;
            resource?.Begin();
        }

        private void NewBackupPopupReverseAnimation(string delay)
        {
            Delay = delay;
            var resource = myWindow.Resources["NewBackupReverseAnimation"] as Storyboard;
            resource?.Begin();
        }

        private void BackupNameWarningAnimation()
        {
            var resource = myWindow.Resources["BackupNameWarningAnimation"] as Storyboard;
            resource?.Begin();
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myWindow_StateChanged(object sender, EventArgs e)
        {
            if (myWindow.WindowState == WindowState.Minimized)
            {
                //newBackupPopup.IsOpen = false;
            }
        }

        private static void DirectoryCopy(string sourcerDirPath, string destDirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcerDirPath);
            if (!dir.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: "+ sourcerDirPath);
        
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirPath))
                Directory.CreateDirectory(destDirPath);
            
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                try
                {
                    //string tempPath = Path.Combine(destDirPath, file.Name);
                    //file.CopyTo(tempPath, false);

                    FileInfo destFile = new FileInfo(Path.Combine(destDirPath, file.Name));
                    if(destFile.Exists)
                    {
                        if (file.LastWriteTime > destFile.LastWriteTime)
                        {
                            file.CopyTo(destFile.FullName, true);
                            Console.WriteLine("Overwriting...");
                        }
                    }
                    else
                    {
                        file.CopyTo(destFile.FullName, true);
                        Console.WriteLine("copying...");
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

        private string OpenFolderBrowser(string delay)
        {
            string path = string.Empty;
            //NewBackupPopupReverseAnimation(delay);
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.Description = "Wybierz folder źródłowy";
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;

            }
            //NewBackupPopupAnimation(delay);

            return path;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == Mouse.LeftButton && e.ClickCount == 2)
            {
                if(WindowState != WindowState.Maximized)
                {
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowState = WindowState.Normal;
                }
            }

            this.DragMove();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    test_host.IsOpen = !test_host.IsOpen;
        //}
    }
}
