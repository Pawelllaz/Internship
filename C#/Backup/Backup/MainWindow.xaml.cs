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
        private List<string> listOfBackupNames;
        private List<string> listOfBackupSourcePaths;
        private List<string> listOfBackupDestPaths;
        private string directoryPath = @"kopia.txt";
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
        private string _fastButton0;
        public string FastButton0
        {
            get
            {
                return _fastButton0;
            }
            set
            {
                _fastButton0 = value;
                OnPropertyChanged(nameof(FastButton0));
            }
        }
        private string _fastButton1;
        public string FastButton1
        {
            get
            {
                return _fastButton1;
            }
            set
            {
                _fastButton1 = value;
                OnPropertyChanged(nameof(FastButton1));
            }
        }
        private string _fastButton2;
        public string FastButton2
        {
            get
            {
                return _fastButton2;
            }
            set
            {
                _fastButton2 = value;
                OnPropertyChanged(nameof(FastButton2));
            }
        }
        private string _fastButton3;
        public string FastButton3
        {
            get
            {
                return _fastButton3;
            }
            set
            {
                _fastButton3 = value;
                OnPropertyChanged(nameof(FastButton3));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            SetButtonsEmpty();
            FIleOperations fIleOperations = new FIleOperations(directoryPath);
            listOfBackupNames = fIleOperations.ReadNames();
            listOfBackupSourcePaths = fIleOperations.ReadSourcePath();
            listOfBackupDestPaths = fIleOperations.ReadDestinationPath();
            
            SetSlots();
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
                BackupNameWarningAnimation();
            else
                newBackupHost.IsOpen = true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            newBackupHost.IsOpen = false;
            BackupName = string.Empty;
            NewBackupSourcePath = string.Empty;
            NewBackupDestinationPath = string.Empty;
        }

        private void MakeNewBackup_Click(object sender, RoutedEventArgs e)
        {
            DirectoryCopy(NewBackupSourcePath, NewBackupDestinationPath);
            newBackupHost.IsOpen = false;

            FIleOperations fIleOperations = new FIleOperations(directoryPath);
            if(listOfBackupNames == null)
                fIleOperations.SaveRecord(BackupName, NewBackupSourcePath, NewBackupDestinationPath);
            else if (!listOfBackupNames.Contains(BackupName))
                fIleOperations.SaveRecord(BackupName, NewBackupSourcePath, NewBackupDestinationPath);
            
            listOfBackupNames = fIleOperations.ReadNames();
            listOfBackupSourcePaths = fIleOperations.ReadSourcePath();
            listOfBackupDestPaths = fIleOperations.ReadDestinationPath();

            BackupName = string.Empty;
            NewBackupSourcePath = string.Empty;
            NewBackupDestinationPath = string.Empty;

            SetSlots();
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

        private void SetSlots()
        {
            Console.WriteLine("ADASDA");
            if (listOfBackupNames != null)
            {
                switch (listOfBackupNames.Count)
                {
                    case 1:
                        FastButton0 = listOfBackupNames.ElementAt(0);
                        break;
                    case 2:
                        FastButton0 = listOfBackupNames.ElementAt(0);
                        FastButton1 = listOfBackupNames.ElementAt(1);
                        break;
                    case 3:
                        FastButton0 = listOfBackupNames.ElementAt(0);
                        FastButton1 = listOfBackupNames.ElementAt(1);
                        FastButton2 = listOfBackupNames.ElementAt(2);
                        break;
                    case 4:
                        FastButton0 = listOfBackupNames.ElementAt(0);
                        FastButton1 = listOfBackupNames.ElementAt(1);
                        FastButton2 = listOfBackupNames.ElementAt(2);
                        FastButton3 = listOfBackupNames.ElementAt(3);
                        break;
                }
            }
        }

        private void SetButtonsEmpty()
        {
            FastButton0 = "pusty slot";
            FastButton1 = "pusty slot";
            FastButton2 = "pusty slot";
            FastButton3 = "pusty slot";
        }

        private void FastButton0_Click(object sender, RoutedEventArgs e)
        {
            int index = listOfBackupNames.IndexOf(FastButton0);
            DirectoryCopy(listOfBackupSourcePaths.ElementAt(index), listOfBackupDestPaths.ElementAt(index));
        }
        private void FastButton1_Click(object sender, RoutedEventArgs e)
        {
            int index = listOfBackupNames.IndexOf(FastButton1);
            DirectoryCopy(listOfBackupSourcePaths.ElementAt(index), listOfBackupDestPaths.ElementAt(index));
        }
        private void FastButton2_Click(object sender, RoutedEventArgs e)
        {
            int index = listOfBackupNames.IndexOf(FastButton2);
            DirectoryCopy(listOfBackupSourcePaths.ElementAt(index), listOfBackupDestPaths.ElementAt(index));
        }
        private void FastButton3_Click(object sender, RoutedEventArgs e)
        {
            int index = listOfBackupNames.IndexOf(FastButton3);
            DirectoryCopy(listOfBackupSourcePaths.ElementAt(index), listOfBackupDestPaths.ElementAt(index));
        }
    }
}
