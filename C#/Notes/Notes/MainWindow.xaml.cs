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


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            NewNoteTxt = "Add note...";

            

        }
        
        private void txtName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewNoteTxt = string.Empty;
            var resourceH = myWindow.Resources["TopPanelAnimation"] as Storyboard;
            resourceH?.Begin();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var resourceH = myWindow.Resources["TopPanelAnimationReverse"] as Storyboard;
            resourceH?.Begin();
        }
    }
}
