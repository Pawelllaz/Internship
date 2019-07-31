using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy Note.xaml
    /// </summary>
    public partial class Note : UserControl, INotifyPropertyChanged
    {
        private string _noteTitle;
        public string NoteTitle
        {
            get
            {
                return _noteTitle;
            }
            set
            {
                _noteTitle = value;
                OnPropertyChanged(nameof(NoteTitle));
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

        public Note()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Focus();
            //popup.IsOpen = false;
            var mousePosition = e.GetPosition(this);
            popup.HorizontalOffset = mousePosition.X - 7;
            popup.VerticalOffset = mousePosition.Y - 7;

            popup.IsOpen = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((removeButton.IsKeyboardFocused != true) && (modifyButton.IsKeyboardFocused != true))
                popup.IsOpen = false;
        }

        private void removeButtonClick(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        // nie wywoluje
        private void modifyButtonClick(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
    }
}

