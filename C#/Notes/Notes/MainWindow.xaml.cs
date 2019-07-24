using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Threading;

namespace Notes
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool flag = true;
        private List<string> FilesPath = new List<string>();
        private string directoryPath = @"C:\notes";
        private string _newNoteTitle;
        public string NewNoteTitle
        {
            get
            {
                return _newNoteTitle;
            }
            set
            {
                _newNoteTitle = value;
                OnPropertyChanged(nameof(NewNoteTitle));
            }
        }
        private string _newNoteTxt;
        public string NewNoteTxt
        {
            get
            {
                return _newNoteTxt;
            }
            set
            {
                _newNoteTxt = value;
                OnPropertyChanged(nameof(NewNoteTxt));
            }
        }
        private string _readedText;
        public string ReadedText
        {
            get
            {
                return _readedText;
            }
            set
            {
                _readedText = value;
                OnPropertyChanged(nameof(ReadedText));
            }
        }



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            NewNoteTitle = "Add note title...";
            NewNoteTxt = "Add note text...";

            Thread thread = new Thread(new ThreadStart(Worker));
            thread.Start();

            if (!CreateDirectory())
            {
                Application.Current.Shutdown();
            }


        }

        private void WriteNote(string path, string textToWrite)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    File.WriteAllText(path, textToWrite);
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
        
        private bool CreateDirectory()
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

        private void TitleName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EnableButtons();
            NewNoteTitle = string.Empty;
            var resourceH = myWindow.Resources["TopPanelAnimation"] as Storyboard;
            resourceH?.Begin();
        }

        private void TxtName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewNoteTxt = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string MakeFilePath()
        {
            string filePath = directoryPath;
            filePath += "\\" + NewNoteTitle + ".txt";
            return filePath;
        }

        private void EnableButtons()
        {
            addButton.IsEnabled = true;
            clearButton.IsEnabled = true;
        }
        private void DisableButtons()
        {
            addButton.IsEnabled = false;
            clearButton.IsEnabled = false;
        }
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            string filePath = null;
            if (!String.IsNullOrEmpty(NewNoteTitle))
            {
                filePath = MakeFilePath();
                WriteNote(filePath, NewNoteTxt);
            }
            var resourceH = myWindow.Resources["TopPanelAnimationReverse"] as Storyboard;
            resourceH?.Begin();

            NewNoteTxt = "Add note text...";
            NewNoteTitle = "Add note title...";
            
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            var resourceH = myWindow.Resources["TopPanelAnimationReverse"] as Storyboard;
            resourceH?.Begin();

            NewNoteTxt = "Add note text...";
            NewNoteTitle = "Add note title...";
        }


        // creating note
        private void CreateNewNote(string path)
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
            
            Grid grid = new Grid();
            grid.Width = 200;
            grid.Height = 300;
            grid.Background = Brushes.Yellow;
            grid.Margin = new Thickness(5);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = textReaded;

            grid.Children.Add(textBlock);
            myWrapPanel.Children.Add(grid);
        }

        private void Worker()
        {
            while (flag)
            {

                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                {
                    string[] files = null;
                    if (Directory.Exists(directoryPath))
                    {
                        files = Directory.GetFiles(directoryPath);
                    }
                    foreach (string fileName in files)
                    {
                        if (!FilesPath.Contains(fileName))
                        {
                            FilesPath.Add(fileName);
                            CreateNewNote(fileName);
                        }
                    }
                
                });
                Thread.Sleep(2000);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            flag = false;
        }
    }
}
