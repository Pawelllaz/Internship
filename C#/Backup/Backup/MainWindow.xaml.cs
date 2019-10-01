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
using System.Threading;


namespace Backup
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private long maxLength;
        private long currentLength;
        private int listIterator;
        private bool workingFlag;
        private bool delayOn;
        private List<string> listOfBackupNames;
        private List<string> listOfBackupSourcePaths;
        private List<string> listOfBackupDestPaths;
        private List<string> listOfDates;
        private string ProgramDataPath = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Dane kopia zapasowa\\Program data.txt");
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
            workingFlag = false;

            
            delayOn = false;
            SetButtonsEmpty();
            FIleOperations fIleOperations = new FIleOperations(ProgramDataPath);
            listOfBackupNames = fIleOperations.ReadNames();
            listOfBackupSourcePaths = fIleOperations.ReadSourcePath();
            listOfBackupDestPaths = fIleOperations.ReadDestinationPath();
            listOfDates = fIleOperations.ReadDate();
            listIterator = 0;

            SetSlots();
            RefreshMyBackups();
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
            if (!workingFlag)
            {
                if (Directory.Exists(NewBackupSourcePath) && Directory.Exists(NewBackupDestinationPath))
                {
                    workingFlag = true;

                    Thread thread = new Thread(() => CopyWorker(NewBackupSourcePath, NewBackupDestinationPath));
                    thread.Start();
                    CopyingProgressBar.Visibility = Visibility.Visible;
                    ProgressAnimation();

                    newBackupHost.IsOpen = false;

                    FIleOperations fIleOperations = new FIleOperations(ProgramDataPath);

                    fIleOperations.SaveRecord(BackupName, NewBackupSourcePath, NewBackupDestinationPath);

                    AddListsRecord(BackupName, NewBackupSourcePath, NewBackupDestinationPath);

                    BackupName = string.Empty;
                    NewBackupSourcePath = string.Empty;
                    NewBackupDestinationPath = string.Empty;

                    SetSlots();
                }
                else
                {
                    NewBackupCopyingWarning.Text = "Wprowadź ścieżkę źródłową i docelową";
                    NewBackupCopyingWarningAnimation();
                }
            }
            else
            {
                NewBackupCopyingWarningAnimation();
                NewBackupCopyingWarning.Text = "Zaczekaj, trwa kopiowanie!";
            }
        }

        private void FastButtonWorker(string fastButtonName)
        {
            if (!workingFlag)
            {
                workingFlag = true;
                int index;
                if (listOfBackupNames != null)
                {
                    index = listOfBackupNames.IndexOf(fastButtonName);
                    if (index != -1)
                    {
                        string backupName = listOfBackupNames.ElementAt(index);
                        string sourcePath = listOfBackupSourcePaths.ElementAt(index);
                        string destPath = listOfBackupDestPaths.ElementAt(index);

                        if (Directory.Exists(sourcePath) && Directory.Exists(destPath))
                        {
                            Thread thread = new Thread(() => CopyWorker(sourcePath, destPath));
                            thread.Start();
                            CopyingProgressBar.Visibility = Visibility.Visible;
                            ProgressAnimation();

                            FIleOperations fIleOperations = new FIleOperations(ProgramDataPath);
                            fIleOperations.SaveRecord(backupName, sourcePath, destPath);
                            AddListsRecord(backupName, sourcePath, destPath);
                            RefreshMyBackups();
                        }
                        else
                            workingFlag = false;
                    }
                    else
                        workingFlag = false;
                }
                else
                    workingFlag = false;
            }
            else
                AmountCopyingWarningAnimation();
        }
        private void FastButton0_Click(object sender, RoutedEventArgs e)
        {
            FastButtonWorker(FastButton0);
        }
        private void FastButton1_Click(object sender, RoutedEventArgs e)
        {
            FastButtonWorker(FastButton1);
        }
        private void FastButton2_Click(object sender, RoutedEventArgs e)
        {
            FastButtonWorker(FastButton2);
        }
        private void FastButton3_Click(object sender, RoutedEventArgs e)
        {
            FastButtonWorker(FastButton3);
        }

        private void AllBackupButton_Click(object sender, RoutedEventArgs e)
        {
            AllBackupsHost.IsOpen = true;
        }
        private void CloseListButton_Click(object sender, RoutedEventArgs e)
        {
            AllBackupsHost.IsOpen = false;
        }

        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteDataHost.IsOpen = true;
        }

        private void ContinueDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteDataHost.IsOpen = false;
            FIleOperations fIleOperations = new FIleOperations(ProgramDataPath);
            fIleOperations.DeleteData();
            System.Windows.Application.Current.Shutdown();
        }

        private void CancelDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteDataHost.IsOpen = false;
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

        private void BackupNameWarningAnimation()
        {
            var resource = myWindow.Resources["BackupNameWarningAnimation"] as Storyboard;
            resource?.Begin();
        }

        private void ProgressAnimation()
        {
            var resource = myWindow.Resources["ProgressAnimation"] as Storyboard;
            resource?.Begin();
        }

        private void ProgressAnimationReverse()
        {
            var resource = myWindow.Resources["ProgressAnimationReverse"] as Storyboard;
            resource?.Begin();
        }
        
        private void AmountCopyingWarningAnimation()
        {
            var resource = myWindow.Resources["AmountCopyingWarningAnimation"] as Storyboard;
            resource?.Begin();
        }
        
        private void NewBackupCopyingWarningAnimation()
        {
            var resource = myWindow.Resources["NewBackupCopyingWarningAnimation"] as Storyboard;
            resource?.Begin();
        }
        
        private void CopyingFinished()
        {
            var resource = myWindow.Resources["CopyingFinished"] as Storyboard;
            resource?.Begin();
        }

        private void myWindow_StateChanged(object sender, EventArgs e)
        {
            if (myWindow.WindowState == WindowState.Minimized)
            {
                //newBackupPopup.IsOpen = false;
            }
        }

        //////////////////=====>>> HELPERS <<<===========////////////////
        private void MyDelay(int delayValue)
        {
            delayOn = true;
            Thread.Sleep(delayValue);

            delayOn = false;
            if (!delayOn && !workingFlag)
            {
              Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
              {
                  CopyingProgressBar.Visibility = Visibility.Hidden;
                  ProgressAnimationReverse();
              });
            }
        }

        private void GetReadyCalculating(string ProgramDataPath)
        {
            maxLength = 0;
            currentLength = 0;
            DirectoryInfo directoryInfo = new DirectoryInfo(ProgramDataPath);
            maxLength = CalculateDirectorySize(directoryInfo, true);
        }

        private static long CalculateDirectorySize(DirectoryInfo directory, bool includeSubdirectories)
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

        //////////////====>>> COPY METHODS <<<=========//////////
        private void CopyWorker(string SourcePath, string DestPath)
        {
            if (Directory.Exists(SourcePath) && Directory.Exists(DestPath))
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                {
                    RefreshMyBackups();
                    CopyingStatus.Text = String.Format("Kopiowanie plików\nz: " + Path.GetFileName(SourcePath) + "\ndo: " + Path.GetFileName(DestPath));
                    CopyingFileStaticText.Text = "Plik:";
                });
                GetReadyCalculating(SourcePath);
                DirectoryCopy(SourcePath, DestPath);
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                {
                    CopyingStatus.Text = String.Format("Kopiowanie zakończone\nz: " + Path.GetFileName(SourcePath) + "\ndo: " + Path.GetFileName(DestPath));
                    CopyingFinished();
                });
                Thread thread = new Thread(() => MyDelay(5000));
                thread.Start();
            }
            workingFlag = false;
        }
        

        private void DirectoryCopy(string sourcerDirPath, string destDirPath)
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
                            currentLength += file.Length;
                        }
                        else
                            currentLength += file.Length;
                            
                        Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                        {
                            double progress = (currentLength * 100) / maxLength;
                            CopyingProgressBar.Value = progress;
                            CopyPercent.Text = progress.ToString() + "%";
                            CopyingFile.Text = file.Name;
                        });
                    }
                    else
                    {
                        file.CopyTo(destFile.FullName, true);
                        currentLength += file.Length;
                        Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                        {
                            double progress = (currentLength * 100) / maxLength;
                            CopyingProgressBar.Value = progress;
                            CopyPercent.Text = progress.ToString()+"%";
                            CopyingFile.Text = file.Name;
                            long toEnd = maxLength - currentLength;
                            CopyingCapacity.Text = String.Format("Pozostało:\n{0:0} MB", toEnd / Math.Pow(2, 20));
                        });
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
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.Description = "Wybierz folder źródłowy";
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;

            }

            return path;
        }

        ////////////////===>>> LAYOUT <<<===////////////////
        
        private void SetSlots()
        {
            if (listOfBackupNames != null)
            {
                int setSlotIterator = 0;
                for (int i = listOfBackupNames.Count - 1; i >= 0; i--)
                {
                    if (FastButton0 != listOfBackupNames.ElementAt(i) && FastButton1 != listOfBackupNames.ElementAt(i) && FastButton2 != listOfBackupNames.ElementAt(i) && FastButton3 != listOfBackupNames.ElementAt(i))
                    {
                        if (setSlotIterator == 4)
                            break;
                        switch (setSlotIterator++)
                        {
                            case 0:
                                FastButton0 = listOfBackupNames.ElementAt(i);
                                break;
                            case 1:
                                FastButton1 = listOfBackupNames.ElementAt(i);
                                break;
                            case 2:
                                FastButton2 = listOfBackupNames.ElementAt(i);
                                break;
                            case 3:
                                FastButton3 = listOfBackupNames.ElementAt(i);
                                break;
                        }
                    }
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

        private void RefreshMyBackups()
        {
            if (listOfBackupNames != null)
            {
                for (; listIterator < listOfBackupNames.Count; listIterator++)
                {
                    string text = String.Format(listOfBackupNames.ElementAt(listIterator) + ", "+listOfBackupSourcePaths.ElementAt(listIterator) + ", " + listOfDates.ElementAt(listIterator));
                    BackupBorder backupBorder = new BackupBorder();
                    backupBorder.SetText(text);
                    AllBackupsStackPanel.Children.Insert(0, backupBorder.GetBorder());
                }
            }
        }

        private void AddListsRecord(string backupName, string sourcePath, string destPath)
        {
            listOfBackupNames.Add(backupName);
            listOfBackupSourcePaths.Add(sourcePath);
            listOfBackupDestPaths.Add(destPath);
            listOfDates.Add(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
        }
    }
}
