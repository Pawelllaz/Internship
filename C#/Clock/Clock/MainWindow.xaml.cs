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
        public double currentMin = 0;
        private double currentH = 0;

        public double MinuteAngle { get; set; } = 50;
        public double SecondAngle { get; set; }
        public double HAngle { get; set; }

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
            SetMinAngle();
            SetHAngle();
            SetSecAngle();
            DataContext = this;

            Thread thread = new Thread(new ThreadStart(Worker));
            thread.Start();

        }

        private void SetSecAngle()
        {
            SecondAngle = DateTime.Now.Second*6;
            SecondAngle += (double)DateTime.Now.Millisecond/175;
        }

        private void SetHAngle()
        {
            currentH = DateTime.Now.Hour;
            hLine.Angle = currentH * 30;
        }

        private void SetMinAngle()
        {
            currentMin = DateTime.Now.Minute;
            MinuteAngle = currentMin * 6;
        }

        private void Worker()
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
            {
                SetMinAngle();
                SetHAngle();
            });

            while (flag)
            {
                
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                {
                    SetSecAngle();
                    secLine.Angle = SecondAngle;

                    if (DateTime.Now.Minute != currentMin)
                    {
                        SetMinAngle();
                        //var s1 = FindResource("mojaAnimacja") as Storyboard;

                        //foreach (var child in s1.Children)
                        //{
                        //    Storyboard.SetTargetName(child, "minLine");
                        //}
                        //s1.Begin(this);

                        var resource = myWindow.Resources["animation"] as Storyboard;
                        resource?.Begin();

                        if (currentH != DateTime.Now.Hour)
                            SetHAngle();
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

        private void UpdateMinute()
        {
            //DoubleAnimation animation = new DoubleAnimation();

            //animation.From = currentMin * 6;
            //currentMin = DateTime.Now.Minute;
            //animation.To = currentMin * 6 + 6;
            ////animation.AutoReverse = true;
            //animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            ////animation.AccelerationRatio = 0.3;
            //RotateTransform rotateTransform = new RotateTransform();
            //rotateTransform.CenterX = 0;
            //rotateTransform.CenterY = 175;
            //minuteLine.RenderTransform = rotateTransform;
            //rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);

            //DoubleAnimation animation2 = new DoubleAnimation();
            //animation2.BeginTime = new TimeSpan(0, 0, 0, 0, 500);
            //animation2.By = 50;
            //animation2.AutoReverse = true;
            //rotateTransform = new RotateTransform();
            //rotateTransform.CenterX = 0;
            //rotateTransform.CenterY = 175;
            //minuteLine.RenderTransform = rotateTransform;
            //rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation2);

            //animation.Completed += (s, e) =>
            //{
            //    animation.From = currentMin * 6 + 6;
            //    animation.To = currentMin * 6;
            //    animation.Duration = new Duration(TimeSpan.FromSeconds(0.25));
            //    rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            //};
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            flag = false;
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /* public event PropertyChangedEventHandler PropertyChanged;

         protected virtual void OnPropertyChanged(string propertyName)
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         }*/
    }
}
