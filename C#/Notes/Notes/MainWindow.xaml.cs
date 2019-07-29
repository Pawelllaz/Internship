using Notes.Controls;
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
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int gridColumnIterator = 0;
        private List<string> FilesPaths = new List<string>();
        private Dictionary<string, Note> Notes = new Dictionary<string, Note>();
        private string directoryPath = @"C:/notes";
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


        // Main
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            NewNoteTitle = "Add note title...";
            NewNoteTxt = "Add note text...";
            AddNewNotes();
            FileOperations fileOperations = new FileOperations(directoryPath);

            if (!fileOperations.CreateDirectory())
            {
                Application.Current.Shutdown();
            }

        }

        //////////////---> BUTTONS <---///////////////////////////
        // Buttons click
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            WriteNote();
            TopPanelAnimationReverse();
            SetNoteDefaultTxt();
            AddNewNotes();
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            TopPanelAnimationReverse();
            SetNoteDefaultTxt();
        }

        // Buttons operations
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

        ////////////////---> MOUSE EVENTS <---///////////////////////
        private void TitleName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EnableButtons();
            NewNoteTitle = string.Empty;
            TopPanelAnimation();
        }

        private void TxtName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewNoteTxt = string.Empty;
        }

        ////////////////---> HELPERS <---///////////////////////
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetNoteDefaultTxt()
        {
            NewNoteTxt = "Add note text...";
            NewNoteTitle = "Add note title...";
        }

        private void WriteNote()
        {
            if (!String.IsNullOrEmpty(NewNoteTitle))
            {
                FileOperations fileOperations = new FileOperations(directoryPath);
                fileOperations.WriteNoteText(NewNoteTitle, NewNoteTxt);
            }
        }

        private void AddNewNotes()
        {
            NoteOperations noteOperations = new NoteOperations();
            FileOperations fileOperations = new FileOperations(directoryPath);
            string[] files = fileOperations.ReadFilesPaths();

            foreach (string filePath in files)
            {
                if (!FilesPaths.Contains(filePath))
                {
                    FilesPaths.Add(filePath);
                    // layout
                    /*string textReaded = fileOperations.ReadTextFromFile(filePath);
                    string title = Path.GetFileNameWithoutExtension(filePath);
                    Grid newGrid = noteOperations.CreateNewNoteGrid(title, textReaded);
                    Grid.SetColumn(newGrid, GridColumnControl());
                    myWrapPanel.Children.Add(newGrid);*/

                    string textReaded = fileOperations.ReadTextFromFile(filePath);
                    Title = Path.GetFileNameWithoutExtension(filePath);
                  //  Grid newGrid = noteOperations.CreateNewNoteGrid(title, textReaded);
                  //  ChooseColumn(newGrid);
                }
            }
        }

        private void ChooseColumn(Grid grid)
        {
            switch (GridColumnControl())
            {
                case 0:
                    wrap0.Children.Add(grid);
                    break;
                case 1:
                    wrap1.Children.Add(grid);
                    break;
                case 2:
                    wrap2.Children.Add(grid);
                    break;
            }
        }


        // animations
        private void TopPanelAnimation()
        {
            var resource = myWindow.Resources["TopPanelAnimation"] as Storyboard;
            resource?.Begin();
        }
        private void TopPanelAnimationReverse()
        {
            var resource = myWindow.Resources["TopPanelAnimationReverse"] as Storyboard;
            resource?.Begin();
        }

        // grid column controler
        private int GridColumnControl()
        {
            if (++gridColumnIterator == 3)
                gridColumnIterator = 0;
            return gridColumnIterator;
        }

        private void rightButton_Click(object sender, RoutedEventArgs e)
        {
            //popup.IsOpen = !popup.IsOpen;
            var note = new Note();
            Notes.Add("note1", note);
            myWrapPanel.Children.Add(note);
        }

        private void myWindow_LocationChanged(object sender, EventArgs e)
        {
            var offset = popup.HorizontalOffset;
            popup.HorizontalOffset = offset + 1;
            popup.HorizontalOffset = offset;
        }
        
    }
}