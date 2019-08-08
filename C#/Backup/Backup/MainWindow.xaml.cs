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

namespace Backup
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<string> BackupNamesList = new List<string>();
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
            if (String.IsNullOrEmpty(BackupName))
            {
                BackupNameWarningAnimation();
            }
            else
            {
                NewBackupPopupAnimation();
                newBackupPopup.IsOpen = true;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            NewBackupPopupReverseAnimation();
            //newBackupPopup.IsOpen = false;
        }

        private void MakeNewBackup_Click(object sender, RoutedEventArgs e)
        {
            // open file dialog C#


            DirectoryCopy(NewBackupSourcePath, NewBackupDestinationPath);
        }

        /////////==============>>> ANIMATIONS <<<==============//////////
        private void NewBackupPopupAnimation()
        {
            var resource = myWindow.Resources["NewBackupAnimation"] as Storyboard;
            resource?.Begin();
        }

        private void NewBackupPopupReverseAnimation()
        {
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
                newBackupPopup.IsOpen = false;
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
                    string tempPath = Path.Combine(destDirPath, file.Name);
                    file.CopyTo(tempPath, false);
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
    }
}
