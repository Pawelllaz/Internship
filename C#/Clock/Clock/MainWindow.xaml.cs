using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Clock
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        private bool flag = true;
        private double currentMin = 0;
        private double currentH = 0;
        
        /*private string _txt;
        public string Txt
        {
            get => _txt;
            set
            {
                _txt = value;
                OnPropertyChanged(nameof(Txt));
            }
        }*/

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Thread thread = new Thread(new ThreadStart(worker));
            thread.Start();

        }

        private double setSecAngle()
        {
            double angle = 0;
            angle = DateTime.Now.Second*6;
            angle += (double)DateTime.Now.Millisecond/175;
            return angle;
        }

        private void worker()
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
            {
                UpdateClock();
            });

            while (flag)
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate 
                {
                    secLine.Angle = setSecAngle();

                    if (DateTime.Now.Minute != currentMin)
                    {
                        UpdateClock();
                        if (currentH != DateTime.Now.Hour)
                        {
                            currentH = DateTime.Now.Hour;
                            hLine.Angle = currentH * 30;
                        }
                    }
                    //Txt = $"{Txt}a";
                });

                Thread.Sleep(30);
                /*Dispatcher.Invoke(() =>
                {
                    line1.Angle = DateTime.Now.Second;
                });*/

            }
        }

        private void UpdateClock()
        {
            DoubleAnimation animation = new DoubleAnimation();

            animation.From = currentMin * 6;
            currentMin = DateTime.Now.Minute;
            animation.To = currentMin * 6 + 6;
            //animation.AutoReverse = true;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            //animation.AccelerationRatio = 0.3;
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = 0;
            rotateTransform.CenterY = 175;
            minuteLine.RenderTransform = rotateTransform;
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            flag = false;
        }

       /* public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
